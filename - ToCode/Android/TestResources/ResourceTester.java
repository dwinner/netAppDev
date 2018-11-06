public class ResourceTester extends BaseTester 
{	
	public void testXML()
	{
		try
		{
			String x = getEventsFromAnXMLFile(this.mContext);
			reportString(x); 
		}
		catch(Throwable t)
		{
			reportString("error reading xml file:" + t.getMessage());
		}
		
	}
	private String getEventsFromAnXMLFile(Context activity)
	throws XmlPullParserException, IOException
	{
	   StringBuffer sb = new StringBuffer();
	   Resources res = activity.getResources();
	   XmlResourceParser xpp = res.getXml(R.xml.test);
	   
	   xpp.next();
	   int eventType = xpp.getEventType();
	    while (eventType != XmlPullParser.END_DOCUMENT) 
	    {
	        if(eventType == XmlPullParser.START_DOCUMENT) 
	        {
	           sb.append("******Start document");
	        } 
	        else if(eventType == XmlPullParser.START_TAG) 
	        {
	           sb.append("\nStart tag "+xpp.getName());
	        } 
	        else if(eventType == XmlPullParser.END_TAG) 
	        {
	           sb.append("\nEnd tag "+xpp.getName());
	        } 
	        else if(eventType == XmlPullParser.TEXT) 
	        {
	           sb.append("\nText "+xpp.getText());
	        }
	        eventType = xpp.next();
	    }//eof-while
	    sb.append("\n******End document");
	    return sb.toString();
	}//eof-function
	
	public void testRawFile()
	{
		try
		{
			String s = getStringFromRawFile(this.mContext);
			this.reportString(s);
		}
		catch(Throwable t)
		{
			this.reportString("error:" + t.getMessage());
		}
	}
	private String getStringFromRawFile(Context activity)
	   throws IOException
	   {
	      Resources r = activity.getResources();
	      InputStream is = r.openRawResource(R.raw.test);
	      String myText = convertStreamToString(is);
	      is.close();
	      return myText;
	   }

	   private String convertStreamToString(InputStream is)
	   throws IOException
	   {
	      ByteArrayOutputStream baos = new ByteArrayOutputStream();
	      int i = is.read();
	      while (i != -1)
	      {
	         baos.write(i);
	         i = is.read();
	      }
	      return baos.toString();
	   }
	   public void testAssets()
	   {
		   try
		   {
			   String s = getStringFromAssetFile(this.mContext);
			   reportString(s);
		   }
		   catch(Throwable t)
		   {
			   reportString("error:" + t.getMessage());
		   }
	   }
	   
	 //Note: Exceptions are not shown in the code
	   String getStringFromAssetFile(Context activity)
	   throws IOException
	   {
	       AssetManager am = activity.getAssets();
	       InputStream is = am.open("test.txt");
	       String s = convertStreamToString(is);
	       is.close();
	       return s;
	   }
}
