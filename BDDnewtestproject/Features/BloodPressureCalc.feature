Feature: Blood Pressure Category
    I want to categorize blood pressure readings
   Feature: Blood Pressure Categorization

  Scenario: Categorize High Blood Pressure
    Given a blood pressure with systolic value 120 and diastolic value 80
    When I give the blood pressure
    Then the category should be "Ideal Blood Pressure"
  Scenario: Categorize PreHigh Blood Pressure
    Given a blood pressure with systolic value 110 and diastolic value 75
    When I give the blood pressure
    Then the category should be "Pre-High Blood Pressure"
  Scenario: Categorize Ideal Blood Pressure
    Given a blood pressure with systolic value 95 and diastolic value 65
    When I give the blood pressure
    Then the category should be "Ideal Blood Pressure"
  Scenario: Categorize Low Blood Pressure
    Given a blood pressure with systolic value 75 and diastolic value 45
    When I give the blood pressure
    Then the category should be "Low Blood Pressure"