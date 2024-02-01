using FileManagement.Repository;

namespace FileManagement
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
           /* services.AddDbContext<BookStoreContext>(
 options => options.UseSqlServer("Server= MSI-C145\\SQLEXPRESS; Database=BookStore; Integrated Security=True;TrustServerCertificate = Yes"));
           */
            services.AddControllersWithViews();

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddScoped<FileInterface, FileInterfaceImplementation>();

          //  services.AddScoped<BookRepo, BookRepo>();
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

}
