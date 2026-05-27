# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All commands are run from `TakeHomeAssignment/WIFIService/`.

```bash
# Build the solution
dotnet build WIFIService.sln

# Run all tests
dotnet test WIFIService.sln

# Run a single test
dotnet test WIFIService.Application.Tests --filter "FullyQualifiedName~<TestMethodName>"

```

## Architecture

The solution follows **Clean Architecture** with **Domain-Driven Design**. Dependencies point strictly inward.

```
WIFIService.Contracts       → Public HTTP request/response models
WIFIService.Domain          → Entities, value objects, domain logic (no dependencies)
WIFIService.Application     → Use cases, port interfaces, DTOs, Result/Error types
WIFIService.Infrastructure.External → Implements Application interfaces; HTTP clients for external services
WIFIService.Api             → Controllers, validators, middleware, mappings
WIFIService.WireMock        → Stub server simulating external dependencies for local development
```

## Key Patterns

### Result pattern
The application layer never throws for expected business failures. Services return `Result` or `Result<T>` carrying an `Error(ErrorCode, message)`. The controller checks `result.IsSuccess` and uses `BusinessProblemDetailsFactory` to convert a failure into a `ProblemDetails` response.

### Error handling
All error responses conform to **RFC 9457** (`application/problem+json`). Two paths:
- **Expected business failures** → `Result.Failure(...)` in the service → `BusinessProblemDetailsFactory` in the controller
- **Unexpected exceptions / validation failures** → caught by `GlobalExceptionHandler` (`IExceptionHandler`)

`ErrorCodeMapper` is the single place that maps `ErrorCode` enum values to HTTP status codes. Every new `ErrorCode` must have a case there.

### Mapping pipeline
There are two explicit mapping hops, each registered as a Mapster `IRegister` profile:
1. `EnableWifiMappingContractDtoProfile` (Api layer): `EnableWifiRequest` → `EnableWifiServiceDto` — flattens the nested contract into two flat objects
2. `EnableWifiMappingDtoDomainProfile` (Application layer): `EnableWifiServiceDto` → `ServiceOrder` domain entity

Input DTOs are intentionally flat (two objects: a service order DTO + a list of characteristic DTOs) rather than mirroring the nested contract structure.

### Validation
FluentValidation validators live in `WIFIService.Api/Validators/`. `ValidateAndThrowAsync` is called in the controller before mapping; the thrown `ValidationException` is caught by `GlobalExceptionHandler` and returned as a `400 ValidationProblemDetails` with per-field errors.

### Resilience
A global `AddStandardResilienceHandler` (retry × 3, 1 s delay, circuit breaker, timeout) is applied to all HTTP clients via `ConfigureHttpClientDefaults` in `WIFIService.Infrastructure.External/DependencyInjection.cs`.

### Test data builders
Test builders live in `WIFIService.Application.Tests/Builders/`. Each builder has sensible defaults and fluent `With*` methods. Use builders for all test input construction.

## Adding a New Use Case

1. **Contracts** — add request/response records in `WIFIService.Contracts`
2. **Application** — add a service interface + implementation under `WifiProvisioning/<UseCaseName>/`, flat input DTOs, and register in `Application/DependencyInjection.cs`
3. **Api** — add a FluentValidation validator, a Mapster contract→DTO profile, and a controller action; register mappings in `Api/Mappings/MapperDependencyInjection.cs`
4. **ErrorCode** — if new failure modes are needed, add to `ErrorCode` enum and map in `ErrorCodeMapper`
