import pytest
from fastapi.testclient import TestClient
from src.app import app

client = TestClient(app)

# Test GET /activities
def test_get_activities():
    response = client.get("/activities")
    assert response.status_code == 200
    data = response.json()
    assert "Chess Club" in data
    assert "Programming Class" in data
    assert "Gym Class" in data

# Test POST /activities/{activity_name}/signup
@pytest.mark.parametrize("activity,email", [
    ("Chess Club", "newstudent@mergington.edu"),
    ("Programming Class", "newcoder@mergington.edu"),
])
def test_signup_for_activity(activity, email):
    response = client.post(f"/activities/{activity}/signup?email={email}")
    assert response.status_code == 200
    assert f"Signed up {email} for {activity}" in response.json()["message"]

# Test duplicate signup
def test_duplicate_signup():
    activity = "Chess Club"
    email = "michael@mergington.edu"
    response = client.post(f"/activities/{activity}/signup?email={email}")
    assert response.status_code == 400
    assert "already signed up" in response.json()["detail"]

# Test DELETE /activities/{activity_name}/unregister
@pytest.mark.parametrize("activity,email", [
    ("Chess Club", "daniel@mergington.edu"),
    ("Programming Class", "emma@mergington.edu"),
])
def test_unregister_from_activity(activity, email):
    response = client.delete(f"/activities/{activity}/unregister?email={email}")
    assert response.status_code == 200
    assert f"Unregistered {email} from {activity}" in response.json()["message"]

# Test unregister non-existent participant
def test_unregister_nonexistent():
    activity = "Chess Club"
    email = "notfound@mergington.edu"
    response = client.delete(f"/activities/{activity}/unregister?email={email}")
    assert response.status_code == 400
    assert "Participant not found" in response.json()["detail"]

# Test activity not found
@pytest.mark.parametrize("endpoint", [
    "/activities/Unknown/signup?email=test@mergington.edu",
    "/activities/Unknown/unregister?email=test@mergington.edu",
])
def test_activity_not_found(endpoint):
    method = "post" if "signup" in endpoint else "delete"
    response = getattr(client, method)(endpoint)
    assert response.status_code == 404
    assert "Activity not found" in response.json()["detail"]
