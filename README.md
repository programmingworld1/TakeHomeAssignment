## README

### Instructions
1. Have docker installed on your machine
2. Navigate to TakeHomeAssignment\WIFIService
3. Fire the following command: Docker compose up
4. Then, either use postman or use the WIFIService.http file in VS (it can be found in the root of the api-project) to call the API endpoint.

- WIFIService.http: is already setup with two different API endpoint requests, just click on Send request:
![alt text](image-1.png)

- Postman: Your request setup should look like this (you can copy the body from the assignment and paste it, or copy it from the .http file):
![alt text](image-2.png)

### Architecture

### Error Handling

All error responses follow the [RFC 9457 Problem Details](https://www.rfc-editor.org/rfc/rfc9457) standard and are returned as `application/problem+json`.

Error handling is split across two layers:

**Application layer — Result pattern**
The service returns a `Result` instead of throwing for expected business failures. 
Which then is converted to a `ProblemDetails` response. Exceptions should be reserved for truly unexpected situations (bugs), not for predictable outcomes like "speed profile not found". Using Result makes the failure explicit in the method, forces the caller to handle it, and avoids the overhead and hidden control flow of exception throwing. Exceptions are also costy. Also, returning a result or throwing an exception says something about the intent: exceptions for situations you dont expect (bugs), errors for business failures.


**API layer — GlobalExceptionHandler**
Everything else then the result-pattern is caught centrally by the `GlobalExceptionHandler` middleware, which could be bugs (via exceptions) or ValidationExceptions. These are also converted to a problem details response.

### 