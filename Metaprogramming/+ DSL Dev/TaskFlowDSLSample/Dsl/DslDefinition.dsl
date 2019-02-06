<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="57dcda11-0ca5-4bfe-85c7-9fb8be4810cc" Description="Description for AppDevUnited.TaskFlowDSLSample.TaskFlowDSLSample" Name="TaskFlowDSLSample" DisplayName="Task Flow" Namespace="AppDevUnited.TaskFlowDSLSample" ProductName="TaskFlowDSLSample" CompanyName="AppDevUnited" PackageGuid="b968471a-092c-4148-b098-e9f37bfb63e9" PackageNamespace="AppDevUnited.TaskFlowDSLSample" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="5a8c656e-062a-47b6-ac35-9d88b7bad140" Description="Overall base class that provides every element with a Name property which acts as its MonikerKey for serialization." Name="NamedElement" DisplayName="Named Element" InheritanceModifier="Abstract" Namespace="AppDevUnited.TaskFlowDSLSample">
      <Properties>
        <DomainProperty Id="ed8ec972-dfb9-40ea-ab32-82dda4b38202" Description="Description for AppDevUnited.TaskFlowDSLSample.NamedElement.Name" Name="Name" DisplayName="Name" DefaultValue="" IsElementName="true">
          <Notes>
            The XmlPropertyData sets this property as the MonikerKey. It is therefore important that all the Name values are unique before the model is saved to file.
            The IsElementName attribute ensures that this property will be initialized uniquely.
            However, it does not prevent the user from setting different elements to have the same name, which would prevent saving.
            An alternative technique would be to use a property with type /System/Guid as the MonikerKey, which would be less susceptible to interference from the user;
            the downside is that monikers in the saved file would be less readable.
          </Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="b0106c39-353f-492c-ad90-c4375db2a377" Description="Root element in which others are embedded." Name="FlowGraph" DisplayName="Flow Graph" Namespace="AppDevUnited.TaskFlowDSLSample">
      <Notes>The ElementMergeDirectives specify what classes of element can be dropped onto this one in the editor, and what relationships that action will construct.
        Merges can also be invoked through the API, providing an alternative way to create embedding relationships.
      </Notes>
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Notes>Enables comments to be dropped onto the diagram.</Notes>
          <Index>
            <DomainClassMoniker Name="Comment" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>FlowGraphHasComments.Comments</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Notes>Enables swimlanes (representing Actors) to be dropped onto the diagram.</Notes>
          <Index>
            <DomainClassMoniker Name="Actor" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>FlowGraphHasActors.Actors</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="ff9d5087-02d5-455d-a7bb-cdb43659655f" Description="Elements that can be connected by Flow links." Name="FlowElement" DisplayName="Flow Element" InheritanceModifier="Abstract" Namespace="AppDevUnited.TaskFlowDSLSample">
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="e3d9fd64-1db1-406a-a51d-ab04e017b5fe" Description="" Name="Description" DisplayName="Description" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Notes>Enables a Comment to be dropped onto an element and linked to it in one action.</Notes>
          <Index>
            <DomainClassMoniker Name="Comment" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ActorHasFlowElements.Actor/!Actor/FlowGraphHasActors.FlowGraph/!FlowGraph/FlowGraphHasComments.Comments</DomainPath>
            <DomainPath>CommentReferencesSubjects.Comments</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="c6585e45-0b78-490e-848c-b122d4834221" Description="Elements that can be connected by ObjectFlow links." Name="ObjectFlowElement" DisplayName="Object Flow Element" InheritanceModifier="Abstract" Namespace="AppDevUnited.TaskFlowDSLSample">
      <BaseClass>
        <DomainClassMoniker Name="FlowElement" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="5d0b0db7-0f26-474f-85f2-9fb6521bbe41" Description="" Name="Task" DisplayName="Task" Namespace="AppDevUnited.TaskFlowDSLSample">
      <BaseClass>
        <DomainClassMoniker Name="ObjectFlowElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="a4c3a73e-a358-47cc-bd9b-fe712e075136" Description="" Name="NestedDiagram" DisplayName="Nested Diagram" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4c374509-d5cd-4475-b9f8-04a042be6192" Description="" Name="Sort" DisplayName="Sort" DefaultValue="Regular">
          <Type>
            <DomainEnumerationMoniker Name="TaskSort" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="fdbeb627-1e79-48f5-aeaf-a10b9d3b7db0" Description="" Name="StartPoint" DisplayName="Start Point" Namespace="AppDevUnited.TaskFlowDSLSample">
      <BaseClass>
        <DomainClassMoniker Name="FlowElement" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="358be060-ccbd-4f78-b205-9a605341d9c3" Description="" Name="Endpoint" DisplayName="Endpoint" Namespace="AppDevUnited.TaskFlowDSLSample">
      <BaseClass>
        <DomainClassMoniker Name="FlowElement" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="44f82875-8d74-40d4-8455-9f89396dfc3f" Description="" Name="MergeBranch" DisplayName="Merge Branch" Namespace="AppDevUnited.TaskFlowDSLSample">
      <BaseClass>
        <DomainClassMoniker Name="FlowElement" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="8c155385-8d1b-4a12-bda8-7503bcf4bb91" Description="" Name="Synchronization" DisplayName="Synchronization" Namespace="AppDevUnited.TaskFlowDSLSample">
      <BaseClass>
        <DomainClassMoniker Name="FlowElement" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="06af9b86-1699-412b-913b-6853aa6cf05b" Description="" Name="ObjectInState" DisplayName="Object In State" Namespace="AppDevUnited.TaskFlowDSLSample">
      <BaseClass>
        <DomainClassMoniker Name="ObjectFlowElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="d729cde0-8da6-4eba-b469-9f27e21db0dd" Description="" Name="State" DisplayName="State" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="0085ca4c-9ac6-453f-b749-626284ae01ff" Description="Can be attached to any task flow element." Name="Comment" DisplayName="Comment" Namespace="AppDevUnited.TaskFlowDSLSample">
      <Properties>
        <DomainProperty Id="087b287d-b224-421b-a31f-d7d1c095ce57" Description="" Name="Text" DisplayName="Text" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="5454f77c-fc9e-4024-9839-4110f8b01df0" Description="Represented by a swim lane on the diagram." Name="Actor" DisplayName="Actor" Namespace="AppDevUnited.TaskFlowDSLSample">
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Notes>One link is created when a Comment is merged into an Actor:
            - the embedding link TaskFlowDSLSampleGraphHasComments, from the TaskFlowDSLSampleGraph to the Comment.
            The embedding link actually starts from the TaskFlowDSLSampleGraph that owns the Actor,
            so the first part of that path navigates to the owner from the Actor; the last segment defines the link to be created.
            Note that there is no link between the Actor and the Comment.
          </Notes>
          <Index>
            <DomainClassMoniker Name="Comment" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>FlowGraphHasActors.FlowGraph/!FlowGraph/FlowGraphHasComments.Comments</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Notes>Enables swimlanes (representing Actors) to appear on the diagram.</Notes>
          <Index>
            <DomainClassMoniker Name="Actor" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>FlowGraphHasActors.FlowGraph/!FlowGraph/.Actors</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="FlowElement" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ActorHasFlowElements.FlowElements</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="b2b7b734-fc09-46ff-943e-330446e1b8d6" Description="Description for AppDevUnited.TaskFlowDSLSample.Flow" Name="Flow" DisplayName="Flow" Namespace="AppDevUnited.TaskFlowDSLSample">
      <Properties>
        <DomainProperty Id="ad34b2f6-ae5a-4d2a-9f61-12b8df050629" Description="" Name="Guard" DisplayName="Guard" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="0a0241f2-21de-46ff-a933-df5b1e9e0853" Description="Description for AppDevUnited.TaskFlowDSLSample.Flow.FlowFrom" Name="FlowFrom" DisplayName="Flow From" PropertyName="FlowTo" PropertyDisplayName="Flow To">
          <RolePlayer>
            <DomainClassMoniker Name="FlowElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3b65f5c8-531a-4c54-b16b-054b213d8d83" Description="Description for AppDevUnited.TaskFlowDSLSample.Flow.FlowTo" Name="FlowTo" DisplayName="Flow To" PropertyName="FlowFrom" PropertyDisplayName="Flow From">
          <RolePlayer>
            <DomainClassMoniker Name="FlowElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="d32d4e10-1f4b-4cf4-ae2a-16755e0d186d" Description="Description for AppDevUnited.TaskFlowDSLSample.FlowGraphHasComments" Name="FlowGraphHasComments" DisplayName="Flow Graph Has Comments" Namespace="AppDevUnited.TaskFlowDSLSample" IsEmbedding="true">
      <Source>
        <DomainRole Id="57bf2297-ac9d-4a04-8d08-c41a0e61baee" Description="Description for AppDevUnited.TaskFlowDSLSample.FlowGraphHasComments.FlowGraph" Name="FlowGraph" DisplayName="Flow Graph" PropertyName="Comments" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Comments">
          <RolePlayer>
            <DomainClassMoniker Name="FlowGraph" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="5f8f9872-cc31-4716-be13-812316f67c80" Description="Description for AppDevUnited.TaskFlowDSLSample.FlowGraphHasComments.Comment" Name="Comment" DisplayName="Comment" PropertyName="FlowGraph" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Flow Graph">
          <RolePlayer>
            <DomainClassMoniker Name="Comment" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="0c75aa85-c994-4f98-b548-b148701b3256" Description="The set of actors (represented by swim lanes) in the task flow." Name="FlowGraphHasActors" DisplayName="Flow Graph Has Actors" Namespace="AppDevUnited.TaskFlowDSLSample" IsEmbedding="true">
      <Source>
        <DomainRole Id="7d1adfec-1d31-4783-a9bd-d6b893dc1930" Description="Description for AppDevUnited.TaskFlowDSLSample.FlowGraphHasActors.FlowGraph" Name="FlowGraph" DisplayName="Flow Graph" PropertyName="Actors" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Actors">
          <RolePlayer>
            <DomainClassMoniker Name="FlowGraph" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="6d4ae8be-27b7-4126-a962-2040ddf32f50" Description="Description for AppDevUnited.TaskFlowDSLSample.FlowGraphHasActors.Actor" Name="Actor" DisplayName="Actor" PropertyName="FlowGraph" Multiplicity="One" PropertyDisplayName="Flow Graph">
          <RolePlayer>
            <DomainClassMoniker Name="Actor" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="4838db37-d729-4d90-b3c8-83c2835af31d" Description="Description for AppDevUnited.TaskFlowDSLSample.CommentReferencesSubjects" Name="CommentReferencesSubjects" DisplayName="Comment References Subjects" Namespace="AppDevUnited.TaskFlowDSLSample">
      <Source>
        <DomainRole Id="90664665-95fe-47a2-abf0-848f51383006" Description="Description for AppDevUnited.TaskFlowDSLSample.CommentReferencesSubjects.Comment" Name="Comment" DisplayName="Comment" PropertyName="Subjects" PropertyDisplayName="Subjects">
          <RolePlayer>
            <DomainClassMoniker Name="Comment" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="5836c2ac-4844-466b-8c78-594ae6ff0271" Description="Description for AppDevUnited.TaskFlowDSLSample.CommentReferencesSubjects.Subject" Name="Subject" DisplayName="Subject" PropertyName="Comments" PropertyDisplayName="Comments">
          <RolePlayer>
            <DomainClassMoniker Name="FlowElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="e60aefb9-1cff-4989-b736-3e0e493558a0" Description="Relationship between Tasks and ObjectInStates." Name="ObjectFlow" DisplayName="Object Flow" Namespace="AppDevUnited.TaskFlowDSLSample">
      <Notes>Although this relationship allows links between any pair of elements, it is intended that there should be an ObjectInState at one end or both.
        The ConnectionBuilder for this relationship therefore imposes that restriction: an ObjectFlow link that doesn't involve an ObjectInState at either end
        can therefore not be constructed using the editor. However, it could be constructed through the API.
      </Notes>
      <Source>
        <DomainRole Id="000cb805-7580-4374-ac8e-a2ead464e590" Description="Description for AppDevUnited.TaskFlowDSLSample.ObjectFlow.ObjectFlowTo" Name="ObjectFlowTo" DisplayName="Object Flow To" PropertyName="ObjectFlowFrom" PropertyDisplayName="Object Flow From">
          <RolePlayer>
            <DomainClassMoniker Name="ObjectFlowElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="d8cfa0c4-b01f-46f1-a98d-0e6bd94ea212" Description="Description for AppDevUnited.TaskFlowDSLSample.ObjectFlow.ObjectFlowFrom" Name="ObjectFlowFrom" DisplayName="Object Flow From" PropertyName="ObjectFlowTo" PropertyDisplayName="Object Flow To">
          <RolePlayer>
            <DomainClassMoniker Name="ObjectFlowElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="7841215f-2f4c-43a6-b363-055ea1e335b5" Description="Description for AppDevUnited.TaskFlowDSLSample.ActorHasFlowElements" Name="ActorHasFlowElements" DisplayName="Actor Has Flow Elements" Namespace="AppDevUnited.TaskFlowDSLSample" IsEmbedding="true">
      <Source>
        <DomainRole Id="9f2f7e45-448a-4e3d-8ec0-c65aaac6fd83" Description="Description for AppDevUnited.TaskFlowDSLSample.ActorHasFlowElements.Actor" Name="Actor" DisplayName="Actor" PropertyName="FlowElements" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Flow Elements">
          <RolePlayer>
            <DomainClassMoniker Name="Actor" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="74babe19-bae8-472d-8d17-97baa35529f1" Description="Description for AppDevUnited.TaskFlowDSLSample.ActorHasFlowElements.FlowElement" Name="FlowElement" DisplayName="Flow Element" PropertyName="Actor" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Actor">
          <RolePlayer>
            <DomainClassMoniker Name="FlowElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <DomainEnumeration Name="TaskSort" Namespace="AppDevUnited.TaskFlowDSLSample" Description="Description for AppDevUnited.TaskFlowDSLSample.TaskSort">
      <Literals>
        <EnumerationLiteral Description="Description for AppDevUnited.TaskFlowDSLSample.TaskSort.Regular" Name="Regular" Value="0" />
        <EnumerationLiteral Description="Description for AppDevUnited.TaskFlowDSLSample.TaskSort.SuperTask" Name="SuperTask" Value="1" />
      </Literals>
    </DomainEnumeration>
  </Types>
  <Shapes>
    <GeometryShape Id="6c602ff5-451a-4f1c-851c-313f538f4ba3" Description="Description for AppDevUnited.TaskFlowDSLSample.TaskShape" Name="TaskShape" DisplayName="Task Shape" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Task Shape" FillColor="213, 231, 205" InitialWidth="1.2" InitialHeight="0.35" OutlineThickness="0.01" FillGradientMode="Vertical" Geometry="RoundedRectangle">
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerBottomRight" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="SubTask" DisplayName="Sub Task" DefaultIcon="resources\SubTaskIcon.bmp" />
      </ShapeHasDecorators>
    </GeometryShape>
    <GeometryShape Id="1afaf6ee-0aa0-446e-b22f-41958e1b4cd0" Description="Description for AppDevUnited.TaskFlowDSLSample.CommentBoxShape" Name="CommentBoxShape" DisplayName="Comment Box Shape" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Comment Box Shape" FillColor="255, 255, 204" OutlineColor="204, 204, 102" InitialHeight="0.3" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Comment" DisplayName="Comment" DefaultText="Comment" />
      </ShapeHasDecorators>
    </GeometryShape>
    <ImageShape Id="9c1c904a-2da0-488a-9aa7-03eb68682209" Description="Description for AppDevUnited.TaskFlowDSLSample.MergeBranchShape" Name="MergeBranchShape" DisplayName="Merge Branch Shape" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Merge Branch Shape" InitialWidth="0.5" InitialHeight="0.3" OutlineThickness="0.01" Image="resources\decision.emf" />
    <ImageShape Id="09a56a64-4427-4f41-91e6-76d31c1e4329" Description="Description for AppDevUnited.TaskFlowDSLSample.EndShape" Name="EndShape" DisplayName="End Shape" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="End Shape" InitialWidth="0.25" InitialHeight="0.25" OutlineThickness="0.01" Image="Resources\Stop.emf" />
    <ImageShape Id="484a1814-ac9c-4f75-9241-c60bf43434d2" Description="Description for AppDevUnited.TaskFlowDSLSample.StartShape" Name="StartShape" DisplayName="Start Shape" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Start Shape" InitialWidth="0.25" InitialHeight="0.25" OutlineThickness="0.01" Image="Resources\Start.emf" />
    <GeometryShape Id="2490ee53-85db-457e-acbb-acec4c9fe169" Description="Description for AppDevUnited.TaskFlowDSLSample.SyncBarShape" Name="SyncBarShape" DisplayName="Sync Bar Shape" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Sync Bar Shape" FillColor="208, 207, 197" InitialWidth="1" InitialHeight="0.1" OutlineThickness="0.01" FillGradientMode="Vertical" Geometry="Rectangle" />
    <GeometryShape Id="23d94e4a-ae3d-451a-a566-8972cb4b6620" Description="Description for AppDevUnited.TaskFlowDSLSample.ObjectShape" Name="ObjectShape" DisplayName="Object Shape" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Object Shape" InitialWidth="1.2" InitialHeight="0.4" OutlineThickness="0.01" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerBottomCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="State" DisplayName="State" DefaultText="State" FontStyle="Italic" />
      </ShapeHasDecorators>
    </GeometryShape>
    <SwimLane Id="e050afea-0f83-47f4-a3d2-bf48e1657c4a" Description="Description for AppDevUnited.TaskFlowDSLSample.ActorSwimLane" Name="ActorSwimLane" DisplayName="Actor Swim Lane" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Actor Swim Lane" HeaderFillColor="LightBlue" InitialWidth="0" InitialHeight="0" SeparatorThickness="0.01">
      <Decorators>
        <SwimLaneHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
          <TextDecorator Name="ActorName" DisplayName="Actor Name" DefaultText="ActorName" />
        </SwimLaneHasDecorators>
      </Decorators>
    </SwimLane>
  </Shapes>
  <Connectors>
    <Connector Id="46b1a5ff-ad50-4101-ba3d-06a5d5996869" Description="Description for AppDevUnited.TaskFlowDSLSample.CommentConnector" Name="CommentConnector" DisplayName="Comment Connector" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Comment Connector" Color="113, 111, 110" DashStyle="Dot" Thickness="0.01" RoutingStyle="Straight" />
    <Connector Id="bf1ffaea-f399-41e7-9131-961217de2844" Description="Description for AppDevUnited.TaskFlowDSLSample.FlowConnector" Name="FlowConnector" DisplayName="Flow Connector" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Flow Connector" Color="113, 111, 110" TargetEndStyle="EmptyArrow" Thickness="0.01">
      <ConnectorHasDecorators Position="SourceTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="Guard" DisplayName="Guard" DefaultText="Guard" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="e58a774a-6dde-45d5-9f58-303e150d58c0" Description="Description for AppDevUnited.TaskFlowDSLSample.ObjectFlowConnector" Name="ObjectFlowConnector" DisplayName="Object Flow Connector" Namespace="AppDevUnited.TaskFlowDSLSample" FixedTooltipText="Object Flow Connector" Color="113, 111, 110" DashStyle="Dash" TargetEndStyle="EmptyArrow" Thickness="0.01" />
  </Connectors>
  <XmlSerializationBehavior Name="TaskFlowDSLSampleSerializationBehavior" Namespace="AppDevUnited.TaskFlowDSLSample">
    <ClassData>
      <XmlClassData TypeName="NamedElement" MonikerAttributeName="name" SerializeId="true" MonikerElementName="namedElementMoniker" ElementName="namedElement" MonikerTypeName="NamedElementMoniker">
        <DomainClassMoniker Name="NamedElement" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="NamedElement/Name" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="Flow" MonikerAttributeName="" SerializeId="true" MonikerElementName="flowMoniker" ElementName="flow" MonikerTypeName="FlowMoniker">
        <DomainRelationshipMoniker Name="Flow" />
        <ElementData>
          <XmlPropertyData XmlName="guard">
            <DomainPropertyMoniker Name="Flow/Guard" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="FlowGraphHasComments" MonikerAttributeName="" SerializeId="true" MonikerElementName="flowGraphHasCommentsMoniker" ElementName="flowGraphHasComments" MonikerTypeName="FlowGraphHasCommentsMoniker">
        <DomainRelationshipMoniker Name="FlowGraphHasComments" />
      </XmlClassData>
      <XmlClassData TypeName="CommentReferencesSubjects" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentReferencesSubjectsMoniker" ElementName="commentReferencesSubjects" MonikerTypeName="CommentReferencesSubjectsMoniker">
        <DomainRelationshipMoniker Name="CommentReferencesSubjects" />
      </XmlClassData>
      <XmlClassData TypeName="ObjectFlow" MonikerAttributeName="" SerializeId="true" MonikerElementName="objectFlowMoniker" ElementName="objectFlow" MonikerTypeName="ObjectFlowMoniker">
        <DomainRelationshipMoniker Name="ObjectFlow" />
      </XmlClassData>
      <XmlClassData TypeName="FlowGraph" MonikerAttributeName="" SerializeId="true" MonikerElementName="flowGraphMoniker" ElementName="flowGraph" MonikerTypeName="FlowGraphMoniker">
        <DomainClassMoniker Name="FlowGraph" />
        <ElementData>
          <XmlRelationshipData RoleElementName="comments">
            <DomainRelationshipMoniker Name="FlowGraphHasComments" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="actors">
            <DomainRelationshipMoniker Name="FlowGraphHasActors" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="FlowElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="flowElementMoniker" ElementName="flowElement" MonikerTypeName="FlowElementMoniker">
        <DomainClassMoniker Name="FlowElement" />
        <ElementData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="FlowElement/Description" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="flowTo">
            <DomainRelationshipMoniker Name="Flow" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ObjectFlowElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="objectFlowElementMoniker" ElementName="objectFlowElement" MonikerTypeName="ObjectFlowElementMoniker">
        <DomainClassMoniker Name="ObjectFlowElement" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="objectFlowFrom">
            <DomainRelationshipMoniker Name="ObjectFlow" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="Task" MonikerAttributeName="" SerializeId="true" MonikerElementName="taskMoniker" ElementName="task" MonikerTypeName="TaskMoniker">
        <DomainClassMoniker Name="Task" />
        <ElementData>
          <XmlPropertyData XmlName="sort">
            <DomainPropertyMoniker Name="Task/Sort" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="nestedDiagram">
            <DomainPropertyMoniker Name="Task/NestedDiagram" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="StartPoint" MonikerAttributeName="" SerializeId="true" MonikerElementName="startPointMoniker" ElementName="startPoint" MonikerTypeName="StartPointMoniker">
        <DomainClassMoniker Name="StartPoint" />
      </XmlClassData>
      <XmlClassData TypeName="Endpoint" MonikerAttributeName="" SerializeId="true" MonikerElementName="endpointMoniker" ElementName="endpoint" MonikerTypeName="EndpointMoniker">
        <DomainClassMoniker Name="Endpoint" />
      </XmlClassData>
      <XmlClassData TypeName="MergeBranch" MonikerAttributeName="" SerializeId="true" MonikerElementName="mergeBranchMoniker" ElementName="mergeBranch" MonikerTypeName="MergeBranchMoniker">
        <DomainClassMoniker Name="MergeBranch" />
      </XmlClassData>
      <XmlClassData TypeName="Synchronization" MonikerAttributeName="" SerializeId="true" MonikerElementName="synchronizationMoniker" ElementName="synchronization" MonikerTypeName="SynchronizationMoniker">
        <DomainClassMoniker Name="Synchronization" />
      </XmlClassData>
      <XmlClassData TypeName="Comment" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentMoniker" ElementName="comment" MonikerTypeName="CommentMoniker">
        <DomainClassMoniker Name="Comment" />
        <ElementData>
          <XmlPropertyData XmlName="text">
            <DomainPropertyMoniker Name="Comment/Text" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="subjects">
            <DomainRelationshipMoniker Name="CommentReferencesSubjects" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ObjectInState" MonikerAttributeName="" SerializeId="true" MonikerElementName="objectInStateMoniker" ElementName="objectInState" MonikerTypeName="ObjectInStateMoniker">
        <DomainClassMoniker Name="ObjectInState" />
        <ElementData>
          <XmlPropertyData XmlName="state">
            <DomainPropertyMoniker Name="ObjectInState/State" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="Actor" MonikerAttributeName="" SerializeId="true" MonikerElementName="actorMoniker" ElementName="actor" MonikerTypeName="ActorMoniker">
        <DomainClassMoniker Name="Actor" />
        <ElementData>
          <XmlRelationshipData RoleElementName="flowElements">
            <DomainRelationshipMoniker Name="ActorHasFlowElements" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="FlowGraphHasActors" MonikerAttributeName="" SerializeId="true" MonikerElementName="flowGraphHasActorsMoniker" ElementName="flowGraphHasActors" MonikerTypeName="FlowGraphHasActorsMoniker">
        <DomainRelationshipMoniker Name="FlowGraphHasActors" />
      </XmlClassData>
      <XmlClassData TypeName="TaskShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="taskShapeMoniker" ElementName="taskShape" MonikerTypeName="TaskShapeMoniker">
        <GeometryShapeMoniker Name="TaskShape" />
      </XmlClassData>
      <XmlClassData TypeName="CommentBoxShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentBoxShapeMoniker" ElementName="commentBoxShape" MonikerTypeName="CommentBoxShapeMoniker">
        <GeometryShapeMoniker Name="CommentBoxShape" />
      </XmlClassData>
      <XmlClassData TypeName="MergeBranchShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="mergeBranchShapeMoniker" ElementName="mergeBranchShape" MonikerTypeName="MergeBranchShapeMoniker">
        <ImageShapeMoniker Name="MergeBranchShape" />
      </XmlClassData>
      <XmlClassData TypeName="EndShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="endShapeMoniker" ElementName="endShape" MonikerTypeName="EndShapeMoniker">
        <ImageShapeMoniker Name="EndShape" />
      </XmlClassData>
      <XmlClassData TypeName="StartShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="startShapeMoniker" ElementName="startShape" MonikerTypeName="StartShapeMoniker">
        <ImageShapeMoniker Name="StartShape" />
      </XmlClassData>
      <XmlClassData TypeName="SyncBarShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="syncBarShapeMoniker" ElementName="syncBarShape" MonikerTypeName="SyncBarShapeMoniker">
        <GeometryShapeMoniker Name="SyncBarShape" />
      </XmlClassData>
      <XmlClassData TypeName="ObjectShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="objectShapeMoniker" ElementName="objectShape" MonikerTypeName="ObjectShapeMoniker">
        <GeometryShapeMoniker Name="ObjectShape" />
      </XmlClassData>
      <XmlClassData TypeName="ActorSwimLane" MonikerAttributeName="" SerializeId="true" MonikerElementName="actorSwimLaneMoniker" ElementName="actorSwimLane" MonikerTypeName="ActorSwimLaneMoniker">
        <SwimLaneMoniker Name="ActorSwimLane" />
      </XmlClassData>
      <XmlClassData TypeName="CommentConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentConnectorMoniker" ElementName="commentConnector" MonikerTypeName="CommentConnectorMoniker">
        <ConnectorMoniker Name="CommentConnector" />
      </XmlClassData>
      <XmlClassData TypeName="FlowConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="flowConnectorMoniker" ElementName="flowConnector" MonikerTypeName="FlowConnectorMoniker">
        <ConnectorMoniker Name="FlowConnector" />
      </XmlClassData>
      <XmlClassData TypeName="ObjectFlowConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="objectFlowConnectorMoniker" ElementName="objectFlowConnector" MonikerTypeName="ObjectFlowConnectorMoniker">
        <ConnectorMoniker Name="ObjectFlowConnector" />
      </XmlClassData>
      <XmlClassData TypeName="TaskFlowDSLSampleDiagram" MonikerAttributeName="" SerializeId="true" MonikerElementName="taskFlowDSLSampleDiagramMoniker" ElementName="taskFlowDSLSampleDiagram" MonikerTypeName="TaskFlowDSLSampleDiagramMoniker">
        <DiagramMoniker Name="TaskFlowDSLSampleDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="ActorHasFlowElements" MonikerAttributeName="" SerializeId="true" MonikerElementName="actorHasFlowElementsMoniker" ElementName="actorHasFlowElements" MonikerTypeName="ActorHasFlowElementsMoniker">
        <DomainRelationshipMoniker Name="ActorHasFlowElements" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="TaskFlowDSLSampleExplorer" />
  <ConnectionBuilders>
    <ConnectionBuilder Name="FlowBuilder">
      <Notes>This ConnectionBuilder constructs the appropriate type of flow (Flow or ObjectFlow) between Tasks and Objects.</Notes>
      <LinkConnectDirective>
        <Notes>Flows can connect any TaskFlowDSLSampleElement to any other. But in this connection builder, 
        we are choosy about what we connect from and to. For example, you cannot connect from an EndPoint,
        nor to a StartPoint.</Notes>
        <DomainRelationshipMoniker Name="Flow" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Task" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="MergeBranch" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="StartPoint" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Synchronization" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Task" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="MergeBranch" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Endpoint" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Synchronization" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
      <LinkConnectDirective>
        <Notes>Connections *to* an Object, from a Task or an Object.</Notes>
        <DomainRelationshipMoniker Name="ObjectFlow" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Task" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ObjectInState" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ObjectInState" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
      <LinkConnectDirective>
        <Notes>Connections *from* an Object, to a Task. (Object to Object is covered by the other LinkConnectDirective.)</Notes>
        <DomainRelationshipMoniker Name="ObjectFlow" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ObjectInState" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Task" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="CommentReferencesSubjectsBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="CommentReferencesSubjects" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Comment" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="FlowElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="e8ebf232-f59b-40f3-9d1c-dfc7a62ddd82" Description="Description for AppDevUnited.TaskFlowDSLSample.TaskFlowDSLSampleDiagram" Name="TaskFlowDSLSampleDiagram" DisplayName="Flow Diagram" Namespace="AppDevUnited.TaskFlowDSLSample">
    <Class>
      <DomainClassMoniker Name="FlowGraph" />
    </Class>
    <ShapeMaps>
      <ShapeMap>
        <DomainClassMoniker Name="Task" />
        <ParentElementPath>
          <DomainPath>ActorHasFlowElements.Actor/!Actor</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TaskShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="TaskShape/SubTask" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="Task/Sort" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="SuperTask" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <GeometryShapeMoniker Name="TaskShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Comment" />
        <ParentElementPath>
          <DomainPath>FlowGraphHasComments.FlowGraph/!FlowGraph</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CommentBoxShape/Comment" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Comment/Text" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="CommentBoxShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="MergeBranch" />
        <ParentElementPath>
          <DomainPath>ActorHasFlowElements.Actor/!Actor</DomainPath>
        </ParentElementPath>
        <ImageShapeMoniker Name="MergeBranchShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Endpoint" />
        <ParentElementPath>
          <DomainPath>ActorHasFlowElements.Actor/!Actor</DomainPath>
        </ParentElementPath>
        <ImageShapeMoniker Name="EndShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="StartPoint" />
        <ParentElementPath>
          <DomainPath>ActorHasFlowElements.Actor/!Actor</DomainPath>
        </ParentElementPath>
        <ImageShapeMoniker Name="StartShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Synchronization" />
        <ParentElementPath>
          <DomainPath>ActorHasFlowElements.Actor/!Actor</DomainPath>
        </ParentElementPath>
        <GeometryShapeMoniker Name="SyncBarShape" />
      </ShapeMap>
      <SwimLaneMap>
        <DomainClassMoniker Name="Actor" />
        <ParentElementPath>
          <DomainPath>FlowGraphHasActors.FlowGraph/!FlowGraph</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ActorSwimLane/ActorName" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <SwimLane>
          <SwimLaneMoniker Name="ActorSwimLane" />
        </SwimLane>
      </SwimLaneMap>
      <ShapeMap>
        <DomainClassMoniker Name="ObjectInState" />
        <ParentElementPath>
          <DomainPath>ActorHasFlowElements.Actor/!Actor</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ObjectShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ObjectShape/State" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ObjectInState/State" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="ObjectShape" />
      </ShapeMap>
    </ShapeMaps>
    <ConnectorMaps>
      <ConnectorMap>
        <ConnectorMoniker Name="FlowConnector" />
        <DomainRelationshipMoniker Name="Flow" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="FlowConnector/Guard" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Flow/Guard" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="ObjectFlowConnector" />
        <DomainRelationshipMoniker Name="ObjectFlow" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="CommentConnector" />
        <DomainRelationshipMoniker Name="CommentReferencesSubjects" />
      </ConnectorMap>
    </ConnectorMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="tf" EditorGuid="2e5295c2-da4d-4442-980e-2be7412cbdfd">
    <RootClass>
      <DomainClassMoniker Name="FlowGraph" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="TaskFlowDSLSampleSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="TaskFlowDSLSample">
      <ElementTool Name="Task" ToolboxIcon="resources\TaskTool.bmp" Caption="Task" Tooltip="Create a Task" HelpKeyword="CreateTaskF1Keyword">
        <DomainClassMoniker Name="Task" />
      </ElementTool>
      <ElementTool Name="StartPoint" ToolboxIcon="Resources\StartTool.bmp" Caption="Start Point" Tooltip="Create a Start Point" HelpKeyword="CreateStartStateF1Keyword">
        <DomainClassMoniker Name="StartPoint" />
      </ElementTool>
      <ElementTool Name="EndPoint" ToolboxIcon="Resources\EndTool.bmp" Caption="End Point" Tooltip="End Point" HelpKeyword="CreateFinalStateF1Keyword">
        <DomainClassMoniker Name="Endpoint" />
      </ElementTool>
      <ElementTool Name="MergeBranch" ToolboxIcon="resources\MergeBranchTool.bmp" Caption="Merge/Branch" Tooltip="Create a Merge/Branch" HelpKeyword="CreateMergeBranchF1Keyword">
        <DomainClassMoniker Name="MergeBranch" />
      </ElementTool>
      <ElementTool Name="Synchronization" ToolboxIcon="resources\SyncBarTool.bmp" Caption="Synchronization" Tooltip="Create a Synchronization bar" HelpKeyword="Synchronization">
        <DomainClassMoniker Name="Synchronization" />
      </ElementTool>
      <ConnectionTool Name="Flow" ToolboxIcon="resources\FlowTool.bmp" Caption="Flow" Tooltip="Drag between Tasks to create a Flow" HelpKeyword="ConnectFlowF1Keyword">
        <ConnectionBuilderMoniker Name="TaskFlowDSLSample/FlowBuilder" />
      </ConnectionTool>
      <ElementTool Name="ObjectInState" ToolboxIcon="resources\ObjectTool.bmp" Caption="Object In State" Tooltip="Create an Object-In-State" HelpKeyword="CreateObjectInStateF1Keyword">
        <DomainClassMoniker Name="ObjectInState" />
      </ElementTool>
      <ElementTool Name="Comment" ToolboxIcon="resources\CommentTool.bmp" Caption="Comment" Tooltip="Create a Comment" HelpKeyword="CreateCommentF1Keyword">
        <DomainClassMoniker Name="Comment" />
      </ElementTool>
      <ConnectionTool Name="CommentSubjects" ToolboxIcon="resources\CommentConnectorTool.bmp" Caption="Comment Connector" Tooltip="Drag to link a Comment to its subjects" HelpKeyword="ConnectCommentSubjectsF1Keyword">
        <ConnectionBuilderMoniker Name="TaskFlowDSLSample/CommentReferencesSubjectsBuilder" />
      </ConnectionTool>
      <ElementTool Name="Actor" ToolboxIcon="resources\SwimlaneTool.bmp" Caption="Actor swim lane" Tooltip="Create an Actor Swim Lane" HelpKeyword="Actor">
        <DomainClassMoniker Name="Actor" />
      </ElementTool>
    </ToolboxTab>
    <Validation UsesMenu="false" UsesOpen="false" UsesSave="false" UsesLoad="false" />
    <DiagramMoniker Name="TaskFlowDSLSampleDiagram" />
  </Designer>
  <Explorer ExplorerGuid="8ee82237-ba84-4921-ba1b-415d9b8c3f06" Title="TaskFlowDSLSample Explorer">
    <ExplorerBehaviorMoniker Name="TaskFlowDSLSample/TaskFlowDSLSampleExplorer" />
  </Explorer>
</Dsl>