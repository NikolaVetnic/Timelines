@Integration
Feature: POST Nodes

    Scenario: POST Nodes
        Given a Node
        When a POST request is sent to the /Nodes endpoint
        Then the response status code is 201 (Created)
        And the Node is created
