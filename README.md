# quadsolutions-assignment-erik-torck

## Backend

1. Load the backend project QuizServer
2. Build and start the server.
   The server should be running on: http://localhost:5183/

## Frontend

1. Navigate to quiz-client
2. Check if you have the right node version with `node -v` (it should be >= v20.17)
3. Run `npm install`
4. Run `npm run dev`

The URLs are set to use the azure host, if you want to use localhost, find and replace the URLs
Normaly I'd use environment variables for this, but since I generaly dont publish those to github and I am unsure how familiar you are with frontend setups I've decided to leave them as inline URLs.

## My Considerations

### Unit testing

For unit testing I didn't go all-out as it isn't a production server and I dont expect any changes. That being said, I did include some unit tests to show how I would go about to do so. In these tests I've included parts that include business logic (sorted possible answers), to ensure future changes don't leak the correct answer by their order. And I decided to check unhappy flows which I expect to be the most likely.

### Frontend

I've set up the frontend using Next.js, thought my experience with React goes up to version 8. I've focused mainly on code quality for the backend as I assume that is the part you're most interested in. For production frontends I'd put in more consideration into it's framework, architecture and design.

### Functionality

#### Minimal Example

For the functionality, I've only setup 1 set of questions with a small amount set, as I considered this excercise as a minimal PoC. There is no way to refresh questions and not much to fill in, therefore the quiz becomes a bit unexciting.

#### Cache mechanism

A current limit for example is, when the client retrieves questions, and the server restarts, the server will compare a different set of questions to the newly retrieved ones, making it highly unlikely the client will have proper input. As the opentdb endpoint gives out a different set of questions each time the endpoint is queried. There are multiple ways to fix this. Right now I cache them on retrieval with a cache decorator for my repository. I think this is fine for a minimal PoC, but if this were a production ready application, I'd highly recommend adding a database for this application. By storing the data with the application you can add proper IDs make it stateless. If the architecture was enforced to make this a Service Oriented Architecture, I would at the least add a hash to a question set, so we can give the client proper feedback if an error does occur.
