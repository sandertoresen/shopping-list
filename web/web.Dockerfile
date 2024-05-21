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

ENV API_URL="bæbubæbu"
ENV API_PORT="iuiuiuiuiu"


EXPOSE 8080

CMD sh ./start.sh "$API_URL" "$API_PORT"