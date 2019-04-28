using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.DataView;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace WebAppAnalysis
{
    public class Program
    {
        public static readonly string NdataPathEN = Path.Combine(Environment.CurrentDirectory, "Data", "yelp_labelled.txt");
        public static readonly string UdataPathRU = Path.Combine(Environment.CurrentDirectory, "Data", "Trans.txt");
        public static readonly string TdataPathRU = Path.Combine(Environment.CurrentDirectory, "Data", "Tranliterated.txt");
        public static readonly string _modelPath  = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");

        public static MLContext    mlContext;
        public static ITransformer trainedModel;
        public static IDataView    trainingDataView;

        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();     
        }   

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static ITransformer BuildAndTrainModel  (IDataView TrainSet)
        {
            var pipeline = mlContext.Transforms.Text.FeaturizeText(outputColumnName: DefaultColumnNames.Features, inputColumnName: nameof(SentimentData.SentimentText))
                           .Append(mlContext.BinaryClassification.Trainers.FastTree(numLeaves: 50, numTrees: 50, minDatapointsInLeaves: 20));

            trainedModel = pipeline.Fit(TrainSet);
            return trainedModel;
        }

        public static string UseModelWithSingleItem    (string input)
        {
            PredictionEngine<SentimentData, SentimentPrediction> predictionFunction = trainedModel.CreatePredictionEngine<SentimentData, SentimentPrediction>(mlContext);

            var resultprediction = predictionFunction.Predict(new SentimentData { SentimentText = input });

            return (resultprediction.Score <= -2) ? "Негативная" : (resultprediction.Probability >= 0.6 && resultprediction.Score >= 2) ? "Позитивная" : "Нейтральная";
        }
                                                       
        public static void SaveModelAsFile()
        {
            using (var fs = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
                mlContext.Model.Save(trainedModel, fs);
        }

        public static void LoadModelFromFile()
        {
            using (var stream = new FileStream(_modelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                trainedModel = mlContext.Model.Load(stream);
        }

        public static void UpdateDataset()
        {
            string s = Transliteration.Front(File.ReadAllText($"Data/Trans.txt"));
            StreamWriter SW = new StreamWriter(new FileStream("Data/Tranliterated.txt", FileMode.Create, FileAccess.Write));
            SW.Write(s);
            SW.Close();
        }
    }
}