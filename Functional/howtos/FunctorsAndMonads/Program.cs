// See https://aka.ms/new-console-template for more information

using FunctorsAndMonads;

// Functors
var towers = new List<Tower>
{
   new() { Id = 1, Name = "tower-1", Damage = 0 },
   new() { Id = 2, Name = "tower-2", Damage = 0 },
   new() { Id = 3, Name = "tower-3", Damage = 0 }
};
var wellTowers = Result<List<Tower>, string>.Ok(towers);
var upgradedTowers = UpgradeTowers(wellTowers);
upgradedTowers.Val.ForEach(Console.WriteLine);

// Applicative
towers.ForEach(tower =>
{
   var towerRes = Result<Tower, string>.Ok(tower);
   var validatedTower = ValidateTower(towerRes);
   if (validatedTower.IsSuccess && validatedTower.Val)
   {
      Console.WriteLine(tower + " validated");
   }
});

// Monads
var deployed = ProcessAndDeployTower(1);
Console.WriteLine(deployed.Val);

return;

Result<Tower, string> FetchTower(int towerId) => throw new NotImplementedException();

Result<Tower, string> UpgradeTower(Tower aTower) => throw new NotImplementedException();

Result<Tower, string> DeployTower(Tower aTower) => throw new NotImplementedException();

Result<Tower, string> ProcessAndDeployTower(int aTowerId) =>
   FetchTower(aTowerId)
      .Bind(UpgradeTower)
      .Bind(DeployTower);

Result<bool, string> ValidateTower(Result<Tower, string> towerResult)
{
   var validateDamage =
      Result<Func<Tower, bool>, string>.Ok(tower => tower.Damage < 100);
   var validateName =
      Result<Func<Tower, bool>, string>.Ok(tower => tower.Name.Length > 5 && !tower.Name.Contains("BannedWord"));
   var damageValidated = towerResult.Apply(validateDamage);
   var nameValidated = towerResult.Apply(validateName);

   return damageValidated.Bind(damageResult =>
      nameValidated.Map(nameResult => damageResult && nameResult)
   );
}

Result<List<Tower>, string> UpgradeTowers(Result<List<Tower>, string> towersResult)
{
   return towersResult.Map(lTowers =>
      lTowers.Select(tower =>
      {
         tower.Name += " (Upgraded)";
         return tower;
      }).ToList());
}