namespace AirlineNotifyClient
{
    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.WinForms;
    using System.Data;
	using System.Text;
	using AIRLINENOTIFYLib;

    /// <summary>
    ///    Summary description for Form1.
    /// </summary>
    public class AirlineNotifyForm : System.WinForms.Form
    {
        /// <summary>
        ///    Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components;
		private System.WinForms.CheckBox checkBoxPaging;
		private System.WinForms.ListBox listPager;
		private AirlineArrivalPager m_pager = null;

        public AirlineNotifyForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

			// Subscribe to event notifications from
			// the AirlineArrivalPager component
			subscribePaging();
        }

        /// <summary>
        ///    Clean up any resources being used.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            components.Dispose();
        }

        /// <summary>
        ///    Required method for Designer support - do not modify
        ///    the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container ();
			this.listPager = new System.WinForms.ListBox ();
			this.checkBoxPaging = new System.WinForms.CheckBox ();
			//@this.TrayHeight = 0;
			//@this.TrayLargeIcon = false;
			//@this.TrayAutoArrange = true;
			listPager.Location = new System.Drawing.Point (0, 8);
			listPager.Size = new System.Drawing.Size (280, 102);
			listPager.ForeColor = System.Drawing.Color.Chartreuse;
			listPager.Font = new System.Drawing.Font ("Arial", 8, System.Drawing.FontStyle.Bold);
			listPager.TabIndex = 0;
			listPager.BackColor = System.Drawing.Color.Black;
			checkBoxPaging.Checked = true;
			checkBoxPaging.Location = new System.Drawing.Point (8, 120);
			checkBoxPaging.Text = "Enable Paging";
			checkBoxPaging.Size = new System.Drawing.Size (224, 16);
			checkBoxPaging.CheckState = System.WinForms.CheckState.Checked;
			checkBoxPaging.TabIndex = 1;
			checkBoxPaging.CheckedChanged += new System.EventHandler (this.checkBoxPaging_CheckedChanged);
			this.Text = "My Airline Pager";
			this.AutoScaleBaseSize = new System.Drawing.Size (5, 13);
			this.ClientSize = new System.Drawing.Size (288, 149);
			this.Controls.Add (this.checkBoxPaging);
			this.Controls.Add (this.listPager);

}//InitializeComponent
		
		/// <summary>
        ///    This is the handler function that will be called when the
        ///    checkbox is checked(to enable Airline arrival paging) or unchecked 
        ///    (to disable Airline arrival paging)
        /// </summary>
		protected void checkBoxPaging_CheckedChanged (object sender, System.EventArgs e)
		{
			if(checkBoxPaging.Checked)
			{
				// If checked, register the delegate referencing the OnMyPagerNotify
				// to the OnAirlineArrivedEvent list (ICP::Advise)
				m_pager.OnAirlineArrivedEvent +=
							new _IAirlineArrivalPagerEvents_OnAirlineArrivedEventEventHandler(OnMyPagerNotify);

			}/* end if */
			else
			{
				// If Unchecked, remove the delegate referencing the OnMyPagerNotify
				// from the OnAirlineArrivedEvent list (ICP::UnAdvise)
				m_pager.OnAirlineArrivedEvent -=
							new _IAirlineArrivalPagerEvents_OnAirlineArrivedEventEventHandler(OnMyPagerNotify);
	
			}/* end else */
		}
		
		/// <summary>
        ///    Creates an AirlineArrivalPager object and subscribes to
        ///    the OnAirlineArrivedEvent event notification.
        /// </summary>
		void subscribePaging() {
			
			// Create an AirlineArrivalPager object
			m_pager = new AirlineArrivalPager();

			// Add the delegate referencing the OnMyPagerNotify method
			// to the OnAirlineArrivedEvent event list (ICP::Advise)
			m_pager.OnAirlineArrivedEvent +=
							new _IAirlineArrivalPagerEvents_OnAirlineArrivedEventEventHandler(OnMyPagerNotify);

		}/* end subscribePaging */
		

		/// <summary>
        ///    This is the handler function that is referenced by the
        ///    delegate to receive OnAirlineArrivedEvent notifications
        /// </summary>
        /// <param name="strAirline">Indicates the name of the airline that just arrived.</param>
		/// <param name="strTerminal">Indicates the Terminal/Gate where it has arrived.</param>
		public void OnMyPagerNotify(String strAirline, String strTerminal) 
		{
			StringBuilder strDetails = new StringBuilder("Airline ");
			strDetails.Append(strAirline);
			strDetails.Append(" has arrived in ");
			strDetails.Append(strTerminal);
			listPager.InsertItem(0,strDetails);

		}/* end OnMyPagerNotify */

		
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args) 
        {
            Application.Run(new AirlineNotifyForm());

        }//Main

    }
}
