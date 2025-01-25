@Integration
Feature: POST Notes

    Scenario: Create Note
        When a POST request is sent to the /Notes endpoint with a valid payload
        Then the response status code is 201 (Created)
        And the Note is created
