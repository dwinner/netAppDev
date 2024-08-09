namespace GroupJoinOp;

internal class DoorOpened(string name, Gender gender, OpenDirection direction)
{
   public string Name { get; set; } = name;

   public OpenDirection Direction { get; set; } = direction;

   public Gender Gender { get; set; } = gender;
}