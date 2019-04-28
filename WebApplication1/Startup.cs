using Microsoft.ML;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebAppAnalysis.Models;
using Microsoft.Data.DataView;

namespace WebAppAnalysis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Program.mlContext = new MLContext();

          //  Program.trainingDataView = Program.mlContext.Data.LoadFromTextFile<SentimentData>(Program.TdataPathRU, hasHeader: false);

          //  Program.trainedModel = Program.BuildAndTrainModel(Program.trainingDataView);

              Program.LoadModelFromFile();

            // Program.UpdateDataset();

            // Program.SaveModelAsFile();

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
              services.AddDbContext<AnalysisContext>(opt =>opt.UseInMemoryDatabase("AnalysisBase"));
              services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (  env.IsDevelopment()){
                  app.UseDeveloperExceptionPage();}
            else{ app.UseHsts();}

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
