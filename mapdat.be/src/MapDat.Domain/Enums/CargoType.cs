using System;

namespace MapDat.Domain.Enums
{
    public class CargoTypeSizes
    {
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string MeasurementCapacity { get; set; }
    }
    public enum CargoType
    {
        TwentyFtFlatRackContainer = 1,
        TwentyFtHighCubeDryContainers,
        TwentyFtOpenTopContainers,
        TwentyFtStandardDryContainer,
        FortyFtFlatRackContainer,
        FortyFtHighCubeDryContainers,
        FortyFtHighCubeReeferContainer,
        FortyFtOpenTopContainer,
        FortyFtStandardDryContainer,
        FortyFiveFtHighCubeDryContainers,
        FortyFiveFtHighCubeReeferContainer,
        Truck3Axles,
        TruckSemitrailer,
        TruckSemitrailerFlatbed
    }

    public static class CargoTypeSizesData
    {
        public static Dictionary<CargoType, CargoTypeSizes> Sizes = new Dictionary<CargoType, CargoTypeSizes>
        {
            { CargoType.TwentyFtFlatRackContainer, new CargoTypeSizes { Length = "5,935 m", Width = "2,398 m", Height = "2,327 m", MeasurementCapacity = "0,000 mc" } },
            { CargoType.TwentyFtHighCubeDryContainers, new CargoTypeSizes { Length = "5,919 m", Width = "2,340 m", Height = "2,286 m", MeasurementCapacity = "33,000 mc" } },
            { CargoType.TwentyFtOpenTopContainers, new CargoTypeSizes { Length = "5,919 m", Width = "2,340 m", Height = "2,286 m", MeasurementCapacity = "32,000 mc" } },
            { CargoType.TwentyFtStandardDryContainer, new CargoTypeSizes { Length = "5,440 m", Width = "2,294 m", Height = "2,286 m", MeasurementCapacity = "27,900 mc" } },
            { CargoType.FortyFtFlatRackContainer, new CargoTypeSizes { Length = "12,080 m", Width = "2,420 m", Height = "2,103 m", MeasurementCapacity = "0,000 mc" } },
            { CargoType.FortyFtHighCubeDryContainers, new CargoTypeSizes { Length = "12,030 m", Width = "2,350 m", Height = "2,690 m", MeasurementCapacity = "76,000 mc" } },
            { CargoType.FortyFtHighCubeReeferContainer, new CargoTypeSizes { Length = "11,577 m", Width = "2,194 m", Height = "2,509 m", MeasurementCapacity = "67,000 mc" } },
            { CargoType.FortyFtOpenTopContainer, new CargoTypeSizes { Length = "12,043 m", Width = "2,338 m", Height = "2,272 m", MeasurementCapacity = "64,000 mc" } },
            { CargoType.FortyFtStandardDryContainer, new CargoTypeSizes { Length = "12,035 m", Width = "2,350 m", Height = "2,393 m", MeasurementCapacity = "67,000 mc" } },
            { CargoType.FortyFiveFtHighCubeDryContainers, new CargoTypeSizes { Length = "13,556 m", Width = "2,351 m", Height = "2,695 m", MeasurementCapacity = "86,000 mc" } },
            { CargoType.FortyFiveFtHighCubeReeferContainer, new CargoTypeSizes { Length = "13,102 m", Width = "2,286 m", Height = "2,509 m", MeasurementCapacity = "75,400 mc" } },
            { CargoType.Truck3Axles, new CargoTypeSizes { Length = "0,000 m", Width = "0,000 m", Height = "0,000 m", MeasurementCapacity = "0,000 mc" } },
            { CargoType.TruckSemitrailer, new CargoTypeSizes { Length = "0,000 m", Width = "0,000 m", Height = "0,000 m", MeasurementCapacity = "0,000 mc" } },
            { CargoType.TruckSemitrailerFlatbed, new CargoTypeSizes { Length = "0,000 m", Width = "0,000 m", Height = "0,000 m", MeasurementCapacity = "0,000 mc" } }
        };
    }
}
