# docker build -t movies-postgres:dev .
# docker run -p 5432:5432 --name movies-postgres -d movies-postgres:dev

FROM postgres:17.4-alpine

ENV POSTGRES_USER=admin
ENV POSTGRES_PASSWORD=admin
ENV POSTGRES_DB=movies-db

EXPOSE 5432

CMD ["postgres"]