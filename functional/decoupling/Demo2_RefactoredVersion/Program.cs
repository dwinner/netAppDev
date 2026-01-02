using static System.Console;

WriteLine("***Refactored version of demonstration 1.***");
VehicleMaker maker = new();
maker.Validate(EngineType.Electric, BodyType.Sports);

internal enum EngineType
{
   Electric,
   InternalCombustion,
   Hybrid
}

internal enum BodyType
{
   Sports,
   Standard
}

internal record class Engine
{
   //public string Status { get; init; }
   public Engine(EngineType engineType) => Type = engineType;

   private EngineType Type { get; }
   //public Engine(EngineType engineType, string engineStatus = "not set")
   //{
   //    Type = engineType;
   //    Status = engineStatus;
   //}
   //public void Install()
   //{
   //    Status = "installed"; //Error if you use init accessor
   //}

   public override string ToString() =>
      //return Type.ToString();
      $"{Type} engine is installed";
}

internal record class Vehicle
{
   public Vehicle(BodyType body, Engine engine, bool licenseStatus = false)
   {
      Body = body;
      Engine = engine;
      LicenseStatus = licenseStatus;
   }

   private BodyType Body { get; }
   private Engine Engine { get; }
   public bool LicenseStatus { get; set; }

   public void AddLicence()
   {
      LicenseStatus = true;
   }

   public override string ToString() =>
      $"""
       The vehicle's description:
       Engine: {Engine}  
       Body: {Body} 
       The license status: {LicenseStatus}
       """;
}

internal class VehicleMaker
{
   private Engine InstallEngine(EngineType engineType) =>
      //return new Engine(engineType) with { Status = "installed" };
      new(engineType);

   private Vehicle CompleteBody(BodyType bodyType, Engine engine) => new(bodyType, engine);

   private Vehicle AddLicense(Vehicle vehicle) => vehicle with { LicenseStatus = true };

   private void Display(Vehicle vehicle)
   {
      Output.ShowStatus(vehicle);
   }

   public void Validate(EngineType engineType, BodyType bodyType)
   {
      // The following calling sequence is OK
      var engine = InstallEngine(engineType);
      var vehicle = CompleteBody(bodyType, engine);
      vehicle = AddLicense(vehicle);

      //// The following calling sequence causes compile-time error
      //Vehicle vehicle = CompleteBody(bodyType, engine);
      //Engine engine = InstallEngine(engineType);
      //vehicle = AddLicense(vehicle);

      ////  The following calling sequence causes compile-time errors too   
      //Vehicle vehicle = AddLicense(vehicle);
      //vehicle = CompleteBody(bodyType, engine);
      //Engine engine = InstallEngine(engineType);

      Display(vehicle);
      // Some other code, if any
   }
}

internal class Output
{
   public static void ShowStatus(Vehicle vehicle)
   {
      WriteLine(vehicle);
   }
}