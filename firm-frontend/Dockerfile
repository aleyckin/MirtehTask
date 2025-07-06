FROM node:18-alpine

WORKDIR /app

COPY package.json package-lock.json ./
RUN npm ci

COPY . .

RUN npm run build --prod

RUN npm install -g http-server

WORKDIR /app/dist/firm-frontend/browser

EXPOSE 8080

CMD ["http-server", "-p", "8080"]