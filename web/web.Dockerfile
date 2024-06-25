# ---- Build Stage ----
FROM node:18-alpine as build
WORKDIR /app
COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build

# ---- Run Stage ----
FROM node:18-alpine
WORKDIR /app
COPY --from=build /app .

EXPOSE 8000

ENTRYPOINT [ "npm", "run", "preview" ]
# CMD sh ./start.sh "$API_URL"