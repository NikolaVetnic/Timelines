@Integration
Feature: POST Timelines

    Scenario: Create Timeline
        When a POST request is sent to the /Timelines endpoint with a valid payload
        Then the response status code is 201 (Created)
        And the Timeline is created
