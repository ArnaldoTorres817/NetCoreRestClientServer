# .NET Core Vanilla Rest Client and Server
## Usage
### RestClient
Perform a GET Request.
```bash
dotnet run [url]
```
#### Example
```bash
# cd to RestClient folder
cd RestClient/

dotnet run https://jsonplaceholder.typicode.com/todos/1
```
#### Output
```json
<<<<<<< HEAD
OK
=======
>>>>>>> docs: README
{
  "userId": 1,
  "id": 1,
  "title": "delectus aut autem",
  "completed": false
}
```
### RestServer
Start the server at the specified port.
```bash
dotnet run [port]
```
#### Example
```bash
# cd to RestServer folder
cd RestServer/

dotnet run 5000

# Server is listening at port 5000.
```