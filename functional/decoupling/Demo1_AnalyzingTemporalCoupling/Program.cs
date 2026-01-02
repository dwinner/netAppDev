using static System.Console;


WriteLine("***Experimenting Temporal Coupling.***");
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

internal class Engine
{
   public Engine(EngineType engineType, string engineStatus = "not set")
   {
      Type = engineType;
      Status = engineStatus;
   }

   private EngineType Type { get; }
   public string Status { get; set; }

   public void Install()
   {
      Status = "installed";
   }

   public override string ToString() =>
      //return Type.ToString();
      $"{Type} engine is {Status}";
}

internal class Vehicle
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
       License status: {LicenseStatus}
       """;
}

internal class VehicleMaker
{
   private Engine _engine;
   private Vehicle _vehicle;

   private void InstallEngine(EngineType engineType)
   {
      _engine = new Engine(engineType);
   }

   private void CompleteBody(BodyType bodyType)
   {
      // Install the engine before working on the vehicle body.
      _engine.Install();
      //_engine?.Install();
      _vehicle = new Vehicle(bodyType, _engine);
   }

   private void AddLicense()
   {
      _vehicle.AddLicence();
      //_vehicle?.AddLicence();
   }

   private void Display(Vehicle vehicle)
   {
      Output.ShowStatus(vehicle);
   }

   public void Validate(EngineType engineType, BodyType bodyType)
   {
      //// The following calling sequence is OK
      //InstallEngine(engineType);
      //CompleteBody(bodyType);
      //AddLicense();

      //// The following calling sequence causes the run-time error        
      //CompleteBody(bodyType);
      //InstallEngine(engineType);
      //AddLicense();

      //// The following calling sequence also causes the run-time error
      //AddLicense();
      //CompleteBody(bodyType);
      //InstallEngine(engineType);

      Display(_vehicle);
      //Some other code, if any
   }
}

internal class Output
{
   public static void ShowStatus(Vehicle vehicle)
   {
      WriteLine(vehicle);
   }
}