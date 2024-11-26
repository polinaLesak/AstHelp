FROM node:18 as build
USER root

WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install

COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/build /usr/share/nginx/html
COPY ./nginx/default.conf /etc/nginx/conf.d/default.conf

CMD ["/bin/sh", "-c", "envsubst < /usr/share/nginx/html/env-config.js > /usr/share/nginx/html/env-config.js && exec nginx -g 'daemon off;'"]
