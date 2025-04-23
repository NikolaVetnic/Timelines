# Use Node.js as a parent image
FROM node:latest

# Set the working directory
WORKDIR /usr/src/react-app

# Install dependencies
COPY package*.json ./
RUN npm install

# Bundle app source
COPY . .

# Expose the port your app runs on
EXPOSE 3000

# Command to run your app
CMD ["npm", "start"]