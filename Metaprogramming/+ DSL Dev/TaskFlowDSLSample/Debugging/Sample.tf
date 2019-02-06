<?xml version="1.0" encoding="utf-8"?>
<flowGraph xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="097241b7-0bfb-440d-962f-ed98cee1b60f" name="" xmlns="http://schemas.microsoft.com/dsltools/TaskFlowDSLSample">
  <comments>
    <comment Id="f2fcbe9f-a262-4ca1-ac96-4b3ee74bde75" text="A Comment">
      <subjects>
        <taskMoniker name="//Actor2/Task1" />
      </subjects>
    </comment>
  </comments>
  <actors>
    <actor Id="65727094-95e4-4521-a426-da7093783179" name="Actor1">
      <flowElements>
        <startPoint Id="4d98c5d3-fe7c-4657-8219-7f66d72ffebc" name="StartPoint1">
          <flowTo>
            <flow Id="db4517f6-8c46-4154-8e25-7933eae081f8">
              <taskMoniker name="//Actor1/Task1" />
            </flow>
          </flowTo>
        </startPoint>
        <task Id="295c1818-2156-4791-9b36-69251e513e3a" name="Task1">
          <flowTo>
            <flow Id="12a958aa-d0ec-4bea-bc70-30d968cfd234">
              <mergeBranchMoniker name="//Actor1/MergeBranch1" />
            </flow>
          </flowTo>
        </task>
        <mergeBranch Id="82688669-b174-4b8c-bbcc-3265422cb514" name="MergeBranch1">
          <flowTo>
            <flow Id="10798cb8-005f-47ac-b445-2ef518239588">
              <taskMoniker name="//Actor1/Task2" />
            </flow>
            <flow Id="c8c0dbd8-e46c-48e4-8498-6a006054ba87">
              <taskMoniker name="//Actor2/Task1" />
            </flow>
          </flowTo>
        </mergeBranch>
        <task Id="8df2ca15-5452-46f6-966e-fe922b22b515" name="Task2">
          <flowTo>
            <flow Id="cf2d5432-ef82-470c-a466-b673f5175c33">
              <synchronizationMoniker name="//Actor1/Synchronization1" />
            </flow>
          </flowTo>
        </task>
        <synchronization Id="e9dd323e-95f9-4ed9-8e01-c34a8e862afa" name="Synchronization1">
          <flowTo>
            <flow Id="d9c015d2-b4d3-4113-b0b9-ea2989ae919a">
              <endpointMoniker name="//Actor1/EndPoint1" />
            </flow>
          </flowTo>
        </synchronization>
        <endpoint Id="936a5c9b-268d-41bc-afa0-dd98bca39bce" name="EndPoint1" />
      </flowElements>
    </actor>
    <actor Id="e9dc2c58-34b9-4c59-b0c5-ce6a0cd87ba5" name="Actor2">
      <flowElements>
        <task Id="bea2d8b9-e62e-42ab-9e06-70a753d3ef08" name="Task1">
          <flowTo>
            <flow Id="c98af592-b2d1-47b0-93f2-123f819bd934">
              <synchronizationMoniker name="//Actor1/Synchronization1" />
            </flow>
          </flowTo>
        </task>
      </flowElements>
    </actor>
    <actor Id="74eb001d-db2b-47a3-b404-216bd0212481" name="Actor3" />
  </actors>
</flowGraph>