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
        public const Name invalidRange = "Invalid Blood Pressure Range";

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        
        //BP : SP/DP mmHG
        //      100/80 eg.
        //    70-190/ 40-100 
        // SP>>DP
        // HBP: 140-190/90-100
        // PHBP: 120-140/ 80-90
        // IBP: 90-120/60-80
        // LOW: 70-90/40-60
        // Calculate systolic category
        public BPCategory systolicCategory
        {
            get{

                if(Systolic >=70 && Systolic <=90 ){
                    BPCategory.Low;
                } else if(Systolic >90 && Systolic <=120 ){
                    BPCategory.Ideal;
                }else if(Systolic >120 && Systolic <=140 ){
                    BPCategory.PreHigh;
                }else if(Systolic >140 && Systolic <=190 ){
                    BPCategory.High;
                }
                
            }
        }

        // Calculate systolic category
        public BPCategory diastolicCategory
        {
            get{

                if(Systolic >=40 && Systolic <=60 ){
                    BPCategory.Low;
                } else if(Systolic >60 && Systolic <=80 ){
                    BPCategory.Ideal;
                }else if(Systolic >80 && Systolic <=90 ){
                    BPCategory.PreHigh;
                }else if(Systolic >90 && Systolic <=100 ){
                    BPCategory.High;
                }
                
            }
        }

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                // implement as part of project
                //throw new NotImplementedException("not implemented yet");
                
                if(this.diastolicCategory == this.systolicCategory){
                    return diastolicCategory;
                } else{
                    return invalidRange;
                }
                                     
            }
        }
    }
}
