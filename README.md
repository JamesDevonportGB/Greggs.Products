# Greggs.Products
## Introduction
Hello, and thanks for checking out the readme for this technical assessment. Please find below each user story, my current implementations, and suggestions of how I'd improve this test if it were to become a larger application

## User Stories
### User Story 1
**As a** Greggs Fanatic<br/>
**I want to** be able to get the latest menu of products rather than the random static products it returns now<br/>
**So that** I get the most recently available products.

**Acceptance Criteria**<br/>
**Given** a previously implemented data access layer<br/>
**When** I hit a specified endpoint to get a list of products<br/>
**Then** a list or products is returned that uses the data access implementation rather than the static list it current utilises

**Resolution**<br/>
- `Product` has been renamed to `ProductEntity` to better reflect it's intended use
- The `IDataAccess<ProductEntity>` is now added to the DI container. No tests written for this assessment as we are assuming it is fully tested and working
- In the default `Get` endpoint, we call our database and map the results to a view model to return them to the method call (the client)
- Unit tests added to ensure the controller is able to get data from the database and successfully map and return it

### User Story 2
**As a** Greggs Entrepreneur<br/>
**I want to** get the price of the products returned to me in Euros<br/>
**So that** I can set up a shop in Europe as part of our expansion

**Acceptance Criteria**<br/>
**Given** an exchange rate of 1GBP to 1.11EUR<br/>
**When** I hit a specified endpoint to get a list of products<br/>
**Then** I will get the products and their price(s) returned _in euros_

**Resolution**<br/>
- The view model now contains a Currency field, with our base currency (GBP) as it's default
- A currency converter service was added that can perform currency conversions from GBP to specified currency types (such as euros)
- A new `GetEuros()` endpoint has been added which returns the same as the default `Get` but with it's pricing in euros.
- New unit tests added to validate functionality of the currency converter and the new `GetEuros` endpoint

## What I'd improve
Right now, the application is a small proof of concept. My current implementation is keeping within those "minimal" parameters. If this were expected to be a larger application, a couple of changes I would implement:
- Mapping could be managed by a tool such as AutoMapper. This change would improve the applications scalability by making mapping between entities and view models more abstract. Services such as the currency converter would be able to occur without a dependency on the controller's copy of the converter.
- With MediatR we could take this a step further, by building out internal services for endpoints to use that contain only the functionality necessary to do one's job, without needing to import everything into the controller regardless of the frequency of its use.
- The currency converter's exchange rates could come from an external source if we wanted real-time updates, such as a venue or payment gateway operator. We could use that value, or we could save it locally to the product entity itself. If the business demanded more granular control of pricing, such as for regional pricing of products, we could save each individual pricing value (relative to each currency) instead of simply currency conversion by percentage.
