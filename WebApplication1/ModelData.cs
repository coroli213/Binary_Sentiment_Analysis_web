using Microsoft.EntityFrameworkCore;
using Microsoft.ML.Data;


namespace WebAppAnalysis.Models
{
    public class TakinData
    {
        public long   Id            { get; set; }
        public string Sentiment     { get; set; }
        public string SentimentText { get; set; }
    }

    public class AnalysisContext : DbContext
    {
          public AnalysisContext(DbContextOptions<AnalysisContext> options) : base(options) { }

          public DbSet<TakinData> AnalysisItems { get; set; }
    }
}

namespace WebAppAnalysis
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public string SentimentText;

        [LoadColumn(1), ColumnName("Label")]
        public bool Sentiment;
    }

    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        [ColumnName("Probability")]
        public float Probability { get; set; }

        [ColumnName("Score")]
        public float Score { get; set; }
    }
}

