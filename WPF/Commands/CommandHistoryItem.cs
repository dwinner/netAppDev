using System.Windows;

namespace Commands
{
	public class CommandHistoryItem
	{
		public CommandHistoryItem(string commandName, UIElement elementActedOn = null,
			string propertyActedOn = "", object previousState = null)
		{
			CommandName = commandName;
			ElementActedOn = elementActedOn;
			PropertyActedOn = propertyActedOn;
			PreviousState = previousState;
		}

		public string CommandName { get; set; }

		public UIElement ElementActedOn { get; set; }

		public string PropertyActedOn { get; set; }

		public object PreviousState { get; set; }

		public bool CanUndo => ElementActedOn != null && PropertyActedOn != "";

		public void Undo()
		{
			var elementType = ElementActedOn.GetType();
			var property = elementType.GetProperty(PropertyActedOn);
			property.SetValue(ElementActedOn, PreviousState, null);
		}
	}
}