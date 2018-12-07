# RandomUserAPI

# Endpoint
https://newclassrooms20181207022247.azurewebsites.net/api/values

# GET
returns "Welcome! Please send a POST request with RandomUser data as the body, either as a JSON string or a file containing JSON."

# POST
- Set HEADER Content-Type to "application/json"
- Set Body to JSON data located here - https://randomuser.me/api/?results=100
- Set HEADER Accept to "application/json", "text/xml", or "text/plain"

OR
- Consume endpoint https://newclassrooms20181207022247.azurewebsites.net/api/values/postWithFile
- Set HEADER Content-Type to "application/x-www-form-urlencoded"
- Set form-data to file, upload file containing JSON data located here - https://randomuser.me/api/?results=100
- Set HEADER Accept to "application/json", "text/xml", or "text/plain"

