Feature: Blood Pressure Measurement
  In order to measure a patient's blood pressure    
  I want to know their systolic and diastolic pressure value    
Scenario: Measuring High Blood Pressure 
    Given a person with systolic blood pressure 150 and the diastolic blood pressure 95
    When hbp blood pressure range is measured
    Then the result should be High Blood Pressure

