FROM maven:3.6.0-jdk-11-slim AS build
RUN mkdir ~/app
WORKDIR /app
EXPOSE 8761

# COPY "eurekaserver-1.jar" .
COPY ["src", "src"]
COPY "pom.xml" .

RUN mvn package -DskipTests=true


VOLUME [ "maven-repo", "/root/.m2" ]

CMD ["java","-jar","target/eurekaserver-1.jar", "--server.port=8761"]