## Env Setup
### To run the application locally we need to set the below env variables to set up the db connection
```
$env:ConnectionStrings__DefaultConnection="Server=localhost;Port=3306;Database=carwale;User ID=root;Password=<YOUR_PASSWORD>;"
```
```
$env:TEST_DB_CONNECTION_STRING="Server=localhost;Port=3306;Database=carwale_test;User ID=root;Password=<YOUR_PASSWORD>;"
```

## Architecture

```text
    React Client
        │
        │ HTTP / JSON
        ▼
┌─────────────────────┐
│     StocksApi       │
│---------------------│
│    Controller       │
│        ↓            │
│       BAL           │
│        ↓            │
│       DAL           │
└─────────┬───────────┘
          │
          │ gRPC
          ▼
┌────────────────────────┐
│   StocksMicroservice   │
│------------------------│
│    gRPC Service        │
│         ↓              │
│        DAL             │
│         ↓              │
│    Dapper / SQL        │
└──────────┬─────────────┘
           │
           ▼
      ┌──────────┐
      │  MySQL   │
      └──────────┘
```

## Database Schema

```mermaid
erDiagram
    FUEL_TYPES ||--o{ STOCKS : "has"
    MAKES ||--o{ STOCKS : "has"
    CITIES ||--o{ STOCKS : "has"
    STOCKS ||--o{ STOCK_IMAGES : "has"

    FUEL_TYPES {
        INT id PK
        VARCHAR name
    }

    MAKES {
        INT id PK
        VARCHAR name
    }

    CITIES {
        INT id PK
        VARCHAR name
    }

    STOCKS {
        INT id PK
        VARCHAR model_name
        INT make_year
        DECIMAL price
        INT kms
        INT fuel_type FK
        INT make_id FK
        INT city_id FK
    }

    STOCK_IMAGES {
        INT id PK
        INT stock_id FK
        VARCHAR image_url
    }
```