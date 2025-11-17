using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name="Low Blood Pressure")] Low,
        [Display(Name="Ideal Blood Pressure")]  Ideal,
        [Display(Name="Pre-High Blood Pressure")] PreHigh,
        [Display(Name ="High Blood Pressure")]  High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                // Get categories for systolic and diastolic
                var systolicCategory = GetSystolicCategory(Systolic);
                var diastolicCategory = GetDiastolicCategory(Diastolic);
                
                // Return the worst category
                return GetWorstCategory(systolicCategory, diastolicCategory);
            }
        }

        private BPCategory GetSystolicCategory(int systolic)
        {
            if (systolic < 90) return BPCategory.Low;
            if (systolic <= 120) return BPCategory.Ideal;
            if (systolic <= 139) return BPCategory.PreHigh;
            return BPCategory.High;
        }

        private BPCategory GetDiastolicCategory(int diastolic)
        {
            if (diastolic < 60) return BPCategory.Low;
            if (diastolic <= 80) return BPCategory.Ideal;
            if (diastolic <= 89) return BPCategory.PreHigh;
            return BPCategory.High;
        }

        private BPCategory GetWorstCategory(BPCategory systolicCat, BPCategory diastolicCat)
        {
            // Order of severity: Ideal < Low < PreHigh < High
            return (BPCategory)Math.Max((int)systolicCat, (int)diastolicCat);
        }
    }
}