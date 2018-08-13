# portfolioProjectBackend

Part of portfolioProject

REST API for the portfolio. 

### API documentation

#### Projects
- GET api/projects
  > returns all projects added to the database
  
- Get api/projects/{id}
  > returns a project with a specific projectID
  
- POST api/projects
  > adds a project to the database
  > *needs admin status

- PUT api/projects/{id}
  > Modifies a project with the projectID in the database
  > *needs admin status

- DELETE api/projects/{id}
  > Deletes a project with the projectID
  > *needs admin status

#### Tools
- GET api/tools
  > returns all tools added to the database
  
- Get api/tools/{id}
  > returns a tool with a specific toolID
  
- POST api/tools
  > adds a tool to the database
  > *needs admin status

- PUT api/tools/{id}
  > Modifies a tool with the toolID in the database
  > *needs admin status

- DELETE api/tool/{id}
  > Deletes a tool with the toolID
  > *needs admin status
  
#### Users

- POST api/identity/register
  > registers a new user

- POST api/identity/login
  > logs a user in and returns JWT-token.
