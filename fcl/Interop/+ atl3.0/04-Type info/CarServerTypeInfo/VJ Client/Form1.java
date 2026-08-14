import com.ms.wfc.app.*;
import com.ms.wfc.core.*;
import com.ms.wfc.ui.*;
import com.ms.wfc.html.*;
import carservertypeinfo.*;
/**
 * This class can take a variable number of parameters on the command
 * line. Program execution begins with the main() method. The class
 * constructor is not invoked unless an object of type 'Form1' is
 * created in the main() method.
 */
public class Form1 extends Form
{
	// The COM coclass.
	private carservertypeinfo.CoCar m_Car;
	
	// All the interfaces of CoCar.
	private carservertypeinfo.ICreateCar itfCreate;
	private carservertypeinfo.IEngine itfEngine;
	private carservertypeinfo.IStats itfStats;
	
	public Form1()
	{
		// Required for Visual J++ Form Designer support
		initForm();		

		// TODO: Add any constructor code after initForm call
	}

	/**
	 * Form1 overrides dispose so it can clean up the
	 * component list.
	 */
	public void dispose()
	{
		super.dispose();
		components.dispose();
	}

	// Create the CoCar and query for ICreateCar.
	//
	private void m_btnCreate_click(Object source, Event e)
	{
		// Create a new CoCar.
		m_Car = new CoCar();

		// Query for ICreateCar by using a Java cast.
		itfCreate = (ICreateCar)m_Car;

		// Set the pet name by asking the text box for the pet name.
		itfCreate.SetPetName( m_txtName.getText() );

		// Ask for the max speed, and transform the text into an Integer.
		int sp = Integer.parseInt(m_txtMaxSpeed.getText());
		itfCreate.SetMaxSpeed(sp);
		// Now, ask for IStats, and set the label with the max speed.
		itfStats = (IStats)m_Car;
		m_lblSpeed.setText( "Speed of: " + itfStats.GetPetName() );
	}


	private void m_btnStats_click(Object source, Event e)
	{
		// Display everything.
		itfStats = (IStats)m_Car;
		itfStats.DisplayStats();
	}

	private void m_btnSpeedUp_click(Object source, Event e)
	{
		
		int curSp = 0;
		int maxSp = 0;		
		itfEngine = (IEngine)m_Car;
		
		maxSp = itfEngine.GetMaxSpeed();
		
		do	// Zoom!
		{	
			itfEngine.SpeedUp();
			curSp =itfEngine.GetCurSpeed();
			// Add cur speed to listbox.
			String s;
			s = Integer.toString(itfEngine.GetCurSpeed());
			listBoxSpeed.addItem(s);
			
		}while(curSp <= maxSp);
		
		// Need to get IStats
		itfStats = (IStats)m_Car;
	
		// Tell user car blew up...
		MessageBox.show(itfStats.GetPetName() + " Blew Up!", "Lead Foot!", 
						MessageBox.ICONEXCLAMATION);
	}


	private void Form1_click(Object source, Event e)
	{
		
	}

	/**
	 * NOTE: The following code is required by the Visual J++ form
	 * designer.  It can be modified using the form editor.  Do not
	 * modify it using the code editor.
	 */
	Container components = new Container();
	Edit m_txtName = new Edit();
	Edit m_txtMaxSpeed = new Edit();
	Label label1 = new Label();
	Label label2 = new Label();
	Button m_btnCreate = new Button();
	Button m_btnStats = new Button();
	Button m_btnSpeedUp = new Button();
	ListBox listBoxSpeed = new ListBox();
	Label m_lblSpeed = new Label();

	private void initForm()
	{
		this.setText("Visual J++ CoCar Client");
		this.setAutoScaleBaseSize(new Point(5, 13));
		this.setBorderStyle(FormBorderStyle.FIXED_DIALOG);
		this.setClientSize(new Point(453, 221));
		this.setStartPosition(FormStartPosition.CENTER_SCREEN);
		this.addOnClick(new EventHandler(this.Form1_click));

		m_txtName.setLocation(new Point(144, 32));
		m_txtName.setSize(new Point(152, 20));
		m_txtName.setTabIndex(0);
		m_txtName.setText("No Name");

		m_txtMaxSpeed.setLocation(new Point(144, 64));
		m_txtMaxSpeed.setSize(new Point(152, 20));
		m_txtMaxSpeed.setTabIndex(1);
		m_txtMaxSpeed.setText("0");

		label1.setFont(new Font("MS Sans Serif", 12.0f, FontSize.POINTS, FontWeight.BOLD, false, false, false, CharacterSet.DEFAULT, 0));
		label1.setLocation(new Point(8, 24));
		label1.setSize(new Point(80, 24));
		label1.setTabIndex(3);
		label1.setTabStop(false);
		label1.setText("Pet Name");

		label2.setFont(new Font("MS Sans Serif", 12.0f, FontSize.POINTS, FontWeight.BOLD, false, false, false, CharacterSet.DEFAULT, 0));
		label2.setLocation(new Point(8, 64));
		label2.setSize(new Point(120, 32));
		label2.setTabIndex(2);
		label2.setTabStop(false);
		label2.setText("Max Speed");

		m_btnCreate.setLocation(new Point(320, 48));
		m_btnCreate.setSize(new Point(120, 24));
		m_btnCreate.setTabIndex(4);
		m_btnCreate.setText("&Create Car");
		m_btnCreate.addOnClick(new EventHandler(this.m_btnCreate_click));

		m_btnStats.setLocation(new Point(8, 120));
		m_btnStats.setSize(new Point(144, 32));
		m_btnStats.setTabIndex(6);
		m_btnStats.setText("Get &Stats");
		m_btnStats.addOnClick(new EventHandler(this.m_btnStats_click));

		m_btnSpeedUp.setLocation(new Point(8, 168));
		m_btnSpeedUp.setSize(new Point(144, 32));
		m_btnSpeedUp.setTabIndex(5);
		m_btnSpeedUp.setText("&Speed Up");
		m_btnSpeedUp.addOnClick(new EventHandler(this.m_btnSpeedUp_click));

		listBoxSpeed.setLocation(new Point(224, 152));
		listBoxSpeed.setSize(new Point(200, 56));
		listBoxSpeed.setTabIndex(7);
		listBoxSpeed.setText("listBox1");
		listBoxSpeed.setUseTabStops(true);

		m_lblSpeed.setFont(new Font("MS Sans Serif", 12.0f, FontSize.POINTS, FontWeight.BOLD, false, false, false, CharacterSet.DEFAULT, 0));
		m_lblSpeed.setLocation(new Point(216, 96));
		m_lblSpeed.setSize(new Point(208, 48));
		m_lblSpeed.setTabIndex(8);
		m_lblSpeed.setTabStop(false);
		m_lblSpeed.setText("");

		this.setNewControls(new Control[] {
							m_lblSpeed, 
							listBoxSpeed, 
							m_btnSpeedUp, 
							m_btnStats, 
							m_btnCreate, 
							label2, 
							label1, 
							m_txtMaxSpeed, 
							m_txtName});
	}

	/**
	 * The main entry point for the application. 
	 *
	 * @param args Array of parameters passed to the application
	 * via the command line.
	 */
	public static void main(String args[])
	{
		Application.run(new Form1());
	}
}
