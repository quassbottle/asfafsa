# Tests
```bash
# Step 1: Run Users Microservice using IDE

# Step 2: Run these commands
docker compose up --build -d # Runs Kafka
docker compose -f docker-compose.test.yaml --build -d # Runs Test Database

# Step 3: Use Unit Tests section in IDE to run tests
```