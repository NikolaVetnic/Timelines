@Integration
Feature: POST Reminders

    Scenario: Create Reminder
        When a POST request is sent to the /Reminders endpoint with a valid payload
        Then the response status code is 201 (Created)
        And the Reminder is created
