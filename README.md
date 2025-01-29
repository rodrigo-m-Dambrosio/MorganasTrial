# Overview

This solution consists of a Web API, the Umbraco CMS, and the Aspire project to connect the services. The Web API provides endpoints that interact with Umbraco CMS. 
In the Umbraco BackOffice, an endpoint "IsOK" has been added under the Morgana group (`/umbraco/management/api/v1/isOk`).

# Instructions

Verify that the initial project is set to 'MorganasTrial.AppHost'. Run the program, and you should be able to see the Aspire dashboard running at `https://localhost:17169/`.

### Instructions for Verifying the Setup

Verify that both applications are running from the Aspire dashboard. You should be able to view the status of each application and access the console logs for each one.

## Default URLs

- **Web API**  
  The Web API runs by default at:  
  `https://localhost:7065/`

- **Umbraco CMS**  
  The Umbraco CMS is available by default at:  
  `https://localhost:5001/`

- **Umbraco Swagger UI**  
  Access the Umbraco Swagger UI by default at:  
  `https://localhost:5001/umbraco/swagger/index.html?urls.primaryName=Umbraco+Management+API`

# Web API Endpoints

To test each endpoint, you can use a REST client like Bruno or Thunder Client from VSCode.

## GET /Healthcheck

- **Calls Umbraco Endpoint**: `GET /umbraco/management/api/v1/health-check-group`

- **Example Request**: `https://localhost:7065/Healthcheck`

- **Example Response**:
  ```json
  {
    "total": 6,
    "items": [
      {"name": "Configuration"},
      {"name": "Data Integrity"},
      {"name": "Live Environment"},
      {"name": "Permissions"},
      {"name": "Security"},
      {"name": "Services"}
    ]
  }
  ```

## POST /DocumentType

- **Calls Umbraco Endpoint**: `POST /umbraco/management/api/v1/document-type`

- **Example Request Body**:
  ```json
  {
    "alias": "This is an alias",
    "name": "This is the Name",
    "description": "This is the description",
    "icon": "icon-notepad",
    "allowedAsRoot": true,
    "variesByCulture": false,
    "variesBySegment": false,
    "collection": null,
    "isElement": true
  }
  ```

- **Example Response**:
  ```json
  {
    "statusCode": 201,
    "body": "",
    "headers": {
      "umbGeneratedResource": "e259b585-f64a-4930-a197-b6456fa18585",
      "location": "https://localhost:5001/umbraco/management/api/v1/document-type/e259b585-f64a-4930-a197-b6456fa18585",
      "umbNotifications": null
    }
  }
  ```

  The GUID of the created document is returned in the `umbGeneratedResource` field.

- **Example Endpoint**: `https://localhost:7065/DocumentType`

## DELETE /DocumentType/{Id}

- **Calls Umbraco Endpoint**: `DELETE /umbraco/management/api/v1/document-type/{id}`

- **Request**: Replace `{id}` with the ID retrieved from the POST response.

- **Example Request**: 
  `https://localhost:7065/DocumentType/c9a29642-3382-42b9-a0a3-ddc287a0bd4d`

- **Response**: Returns `200` when the document is deleted successfully.

If the document doesn't exist it will return a response like this:
```json
{
  "type": "Error",
  "title": "Not Found",
  "status": 404,
  "detail": "The specified document type was not found",
  "operationStatus": "NotFound"
}
  ```

## GET /isOk

- **Calls Umbraco Endpoint**: `GET /umbraco/management/api/v1/isOk`

- **Request**: 

  - Query parameter `value` should be a boolean (`true` or `false`).

- **Example Requests**: 

  ```plaintext
  GET https://localhost:7065/isOk?value=true
  GET https://localhost:7065/isOk?value=false


 - **Example responses**: 
  - Returns `200 : The API returned: OK` when the value is true.
  - Returns `400 : The API returned: BadRequest` when the value is false.