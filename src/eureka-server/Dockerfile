FROM maven:3.6.0-jdk-11-slim AS build
RUN mkdir ~/app
WORKDIR /app
EXPOSE 8761

COPY ["src/eureka-server/src", "src"]
COPY "src/eureka-server/pom.xml" .

RUN mvn package -DskipTests=true

CMD ["java","-jar","target/eurekaserver-1.jar", "--server.port=8761"]