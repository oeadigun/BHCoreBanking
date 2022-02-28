using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Implementations;
using BHCoreBanking.Data;
using BHCoreBanking.Data.Contracts;
using BHCoreBanking.Data.DataStore;
using BHCoreBanking.Services.Contracts;
using BHCoreBanking.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace BHCoreBanking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var customerStore = new InMemoryStore<ICustomer>();
            var stored = customerStore.SaveAsync(typeof(ICustomer).Name, new List<ICustomer>() { new Customer() { FirstName = "Default", LastName = "Account", ID = 1 } }).Result;

            services.AddSwaggerGen();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            services.AddSingleton<IDataStore<ICustomer>>(t => customerStore); 
            services.AddScoped<IRepository<ICustomer>>(t => new Repository<ICustomer>(t.GetRequiredService<IDataStore<ICustomer>>())); 
            services.AddTransient<ICustomerService>(t => new CustomerService(t.GetRequiredService<IRepository<ICustomer>>()));

            var transactionStore = new InMemoryStore<ITransaction>(); 
            var accountStore = new InMemoryStore<IAccount>();
            stored = accountStore.SaveAsync(typeof(IAccount).Name, new List<IAccount>() { new Account() { CustomerID = 1, AccountNumber = "00000000000", Balance = new Balance() { Amount = 1000, CurrencyCode = "USD", Position = Core.Enums.PositionType.Credit }, Type = Core.Enums.AccountType.Current, ID = 1 } }).Result;

            services.AddSingleton<IDataStore<IAccount>>(t => accountStore);
            services.AddScoped<IRepository<IAccount>>(t => new Repository<IAccount>(t.GetRequiredService<IDataStore<IAccount>>()));

            services.AddSingleton<IDataStore<ITransaction>>(t => transactionStore);
            services.AddScoped<IRepository<ITransaction>>(t => new Repository<ITransaction>(t.GetRequiredService<IDataStore<ITransaction>>()));
            services.AddTransient<ITransactionService>(t => new TransactionService(t.GetRequiredService<IRepository<ITransaction>>(), t.GetRequiredService<IRepository<IAccount>>()));


           services.AddTransient<IAccountService>(t => new AccountService(t.GetRequiredService<IRepository<IAccount>>(), t.GetRequiredService<ITransactionService>()));


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger()
             .UseSwaggerUI(c =>
             {

             });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
