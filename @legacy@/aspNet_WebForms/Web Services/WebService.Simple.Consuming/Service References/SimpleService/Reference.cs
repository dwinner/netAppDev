﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebService.Simple.Consuming.SimpleService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://bloodhound.com/", ConfigurationName="SimpleService.SimpleServiceSoap")]
    public interface SimpleServiceSoap {
        
        // CODEGEN: Контракт генерации сообщений с именем CanWeFixItResult из пространства имен http://bloodhound.com/ не отмечен как обнуляемый
        [System.ServiceModel.OperationContractAttribute(Action="http://bloodhound.com/CanWeFixIt", ReplyAction="*")]
        WebService.Simple.Consuming.SimpleService.CanWeFixItResponse CanWeFixIt(WebService.Simple.Consuming.SimpleService.CanWeFixItRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CanWeFixItRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CanWeFixIt", Namespace="http://bloodhound.com/", Order=0)]
        public WebService.Simple.Consuming.SimpleService.CanWeFixItRequestBody Body;
        
        public CanWeFixItRequest() {
        }
        
        public CanWeFixItRequest(WebService.Simple.Consuming.SimpleService.CanWeFixItRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class CanWeFixItRequestBody {
        
        public CanWeFixItRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CanWeFixItResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CanWeFixItResponse", Namespace="http://bloodhound.com/", Order=0)]
        public WebService.Simple.Consuming.SimpleService.CanWeFixItResponseBody Body;
        
        public CanWeFixItResponse() {
        }
        
        public CanWeFixItResponse(WebService.Simple.Consuming.SimpleService.CanWeFixItResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://bloodhound.com/")]
    public partial class CanWeFixItResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CanWeFixItResult;
        
        public CanWeFixItResponseBody() {
        }
        
        public CanWeFixItResponseBody(string CanWeFixItResult) {
            this.CanWeFixItResult = CanWeFixItResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SimpleServiceSoapChannel : WebService.Simple.Consuming.SimpleService.SimpleServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SimpleServiceSoapClient : System.ServiceModel.ClientBase<WebService.Simple.Consuming.SimpleService.SimpleServiceSoap>, WebService.Simple.Consuming.SimpleService.SimpleServiceSoap {
        
        public SimpleServiceSoapClient() {
        }
        
        public SimpleServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SimpleServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SimpleServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SimpleServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebService.Simple.Consuming.SimpleService.CanWeFixItResponse WebService.Simple.Consuming.SimpleService.SimpleServiceSoap.CanWeFixIt(WebService.Simple.Consuming.SimpleService.CanWeFixItRequest request) {
            return base.Channel.CanWeFixIt(request);
        }
        
        public string CanWeFixIt() {
            WebService.Simple.Consuming.SimpleService.CanWeFixItRequest inValue = new WebService.Simple.Consuming.SimpleService.CanWeFixItRequest();
            inValue.Body = new WebService.Simple.Consuming.SimpleService.CanWeFixItRequestBody();
            WebService.Simple.Consuming.SimpleService.CanWeFixItResponse retVal = ((WebService.Simple.Consuming.SimpleService.SimpleServiceSoap)(this)).CanWeFixIt(inValue);
            return retVal.Body.CanWeFixItResult;
        }
    }
}
