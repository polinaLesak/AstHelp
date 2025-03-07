FROM nginx:latest

RUN apt-get update && apt-get install -y openssl

RUN mkdir -p /etc/nginx/ssl && chmod 777 /etc/nginx/ssl

RUN openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
          -keyout /etc/nginx/ssl/nginx.key \
          -out /etc/nginx/ssl/nginx.crt \
          -subj '/CN=localhost'

RUN rm /etc/nginx/conf.d/default.conf

COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
EXPOSE 443