# Fish Counts API

## Introduction

The Fish Counts API is a RESTful API that provides a simplified view of observation data from the [Environment Agency Ecology and Fish Data API](https://environment.data.gov.uk/ecology/api/v1/index.html).

It calls the ecology API's observations endpoint with a set of filters to retrieve the number of fish counted on each day at a small selection of sites.

## Setup

Prior to attending the paired programming exercise, please follow the steps below to ensure the project is running correctly on your local machine.

During the exercise we will ask you to share your screen to show how you work through the tasks, so please try to ensure that your brower or Teams client has the necessary permissions.

### Prerequisites

- .Net 8.0 SDK
- IDE (Visual Studio / VS Code / Rider etc)

We don't specify a particular OS or IDE to use as long as you're able to complete the setup instructions.

### API Project Setup (src/FishCountsApi)

You should be able to build and run the API project following the usual tools in your IDE.

There are two launch profiles available, Kestrel (default) and IIS Express. Either option will start a local server using the HTTP protocol on port 5244.

Your browser should automatically open the API's Swagger page, but if not you can navigate to [http://localhost:5244/swagger/index.html](http://localhost:5244/swagger/index.html) in your browser of choice.

Expand the `GET /fishCounts` section, click `Try it out` then `Execute`. You should see a response with a list of fish types and their counts.

### Unit Test Project Setup (tests/FishCountsUnitTests)

The unit test project uses mocked dependencies, so does not require the API to be running.

Use the tools provided by your IDE to execute all of the tests in the project and ensure they all pass.

### E2E Test Project Setup (tests/FishCountsE2ETests)

The end-to-end test project executes externally against the API using real downstream services.

Use the tools provided by your IDE to execute all of the tests in the project and ensure they all pass.

### Checklist

- [ ] Browser (or Teams client) has permission to share your screen
- [ ] API project builds and runs
- [ ] API endpoint can be invoked through the Swagger UI
- [ ] Unit tests pass
- [ ] E2E tests pass

## Useful Information

Feel free to take a look at the structure of the code to familiarise yourself with the layout.

You may also wish to look at the [Environment Agency Ecology and Fish Data API](https://environment.data.gov.uk/ecology/api/v1/index.html) documentation. We will only interact with the observations and sites endpoints in this tech test.

The API does not document its responses so here are some examples of objects that are returned:

**Observation**
```
{
    "result_value": 4,
    "date": "2023-12-11",
    "result_unit": "http://qudt.org/schema/qudt/CountingUnit",
    "property_id": "http://environment.data.gov.uk/ecology/def/fish/TotalCount",
    "ultimate_feature_of_interest_label": "Flounder",
    "result_id": "http://environment.data.gov.uk/ecology/result/fish/164459-342-totalCount",
    "property_label": "Total Count",
    "survey_id": "http://environment.data.gov.uk/ecology/sampling/fish/164459",
    "observation_type": "http://environment.data.gov.uk/ecology/def/fish/TracFishSpeciesObservation",
    "ultimate_feature_of_interest_id": "http://environment.data.gov.uk/ecology/species/fish/342",
    "simple_result": 4,
    "number_of_runs": 1,
    "site_id": "http://environment.data.gov.uk/ecology/site/fish/26498",
    "result_datatype": "http://www.w3.org/2001/XMLSchema#integer",
    "observation_id": "http://environment.data.gov.uk/ecology/observation/fish/164459-342-totalCount"
}
```

**Site**
```
{
    "long": -1.293377571,
    "local_id": "13369",
    "northing": 436900,
    "type": "http://environment.data.gov.uk/ecology/def/FishFreshwaterSite",
    "wkt": {
        "raw-value": "POINT (-1.293378 53.82629)",
        "datatype-uri": "http://www.opengis.net/ont/geosparql#wktLiteral"
    },
    "easting": 446610,
    "label": "D/S Crooked Billet",
    "lat": 53.8263,
    "site_id": "http://environment.data.gov.uk/ecology/site/fish/13369"
}
```

## Troubleshooting

### Browser Warnings

If your browser is attempting to redirect you to an HTTPS version of the API, a previous locally running application may have configured an HSTS policy.

In Chrome this can be disabled by navigating to [chrome://net-internals/#hsts](chrome://net-internals/#hsts) and deleting the domain security policy for localhost. You may need to fully close and reopen the browser for the change to take effect.

## Terms of Use

This tech test is confidential and must not be shared with anyone else.

All copies of the solution must be deleted immediately after the conclusion of the paired programming session.


## Task 0
### Add paging support to GET /fishCounts

Before the paired programming session, please complete the following task to ensure the API supports paging.

### Objective

Update the GET /fishCounts endpoint to support paging parameters so that results can be retrieved in smaller, controlled batches.

### Requirements

The endpoint should accept two optional query parameters:

offset   the number of records to skip before starting to return results.

limit   the maximum number of records to return.

Default values may be provided (for example, offset = 0, limit = 50).

Ensure paging is delegated to the downstream Ecology API, not performed in memory.

Validate that both offset and limit are non-negative integers.

If invalid values are provided, the API should return a 400 Bad Request response.

You may use data annotations such as [Range(0, int.MaxValue)] on the query model, or handle validation in another appropriate way.

Update or add any necessary unit tests and end-to-end tests to verify your implementation.

Add or update Swagger documentation to include the new query parameters.

### What We will Discuss

At the start of the paired programming session, you ll be asked to:

Demo your implementation and walk through your approach.

Explain how you handled validation and error responses.

Highlight any changes you made to the tests.

Discuss any considerations or assumptions about the Ecology API s paging behaviour (e.g., maximum page size, missing documentation).