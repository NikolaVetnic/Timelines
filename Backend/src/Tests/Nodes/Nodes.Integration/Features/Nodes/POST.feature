@Integration
Feature: POST Nodes

    Scenario: Create Node
        When a POST request is sent to the /Nodes endpoint with a valid payload
        Then the response status code is 201 (Created)
        And the Node is created
