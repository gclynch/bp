Feature: Blood Pressure Measurement
  In order to measure a patient's blood pressure    
  I want to know their systolic and diastolic pressure value    

@HighBloodPressure
Scenario: Measuring High Blood Pressure 
    Given a person with systolic blood pressure 150
	And the diastolic blood pressure 95
    When their blood pressure range is measured
    Then the result should be High Blood Pressure

@PreHighBloodPressure
Scenario: Measuring Pre-High Blood Pressure
    Given a person having systolic blood pressure 130 
	And the diastolic blood pressure 85
	When their blood pressure range is measured
	Then the result should be Pre-High Blood Pressure

@IdealBloodPressure
Scenario: Measuring Ideal Blood Pressure
    Given a person with systolic blood pressure 110 
	And the diastolic blood pressure 70
	When their blood pressure range is measured
	Then the result should be Ideal Blood Pressure

@LowBloodPressure
Scenario: Measuring Low Blood Pressure
    Given a person with systolic blood pressure 80 
	And the diastolic blood pressure 50
    When their blood pressure range is measured
    Then the result should be Low Blood Pressure
