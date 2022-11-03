pipeline {
  agent any
  stages {
    stage('Checkout code') {
      steps {
        git(url: 'https://github.com/aramzham/AllTestingStuff', branch: 'master')
      }
    }

    stage('Add log message') {
      parallel {
        stage('Add log message') {
          steps {
            sh 'ls -la'
          }
        }

        stage('Parallel step') {
          steps {
            sh '''var="Hello World"
 
# print it 
echo "$var"
 
# Another way of printing it
printf "%s\\n" "$var"'''
          }
        }

      }
    }

    stage('Build') {
      steps {
        sh '''cd Experimenting/CachingInDotNet7/
docker build -f Dockerfile . -t 128901/CachingInDotNet7:latest'''
      }
    }

    stage('Log into Dockerhub') {
      environment {
        DOCKERHUB_USER = 'xxx'
        DOCKERHUB_PASSWORD = 'xxx'
      }
      steps {
        sh 'docker login -u $DOCKERHUB_USER -p $DOCKERHUB_PASSWORD'
      }
    }

    stage('Push to Dockerhub') {
      steps {
        sh 'docker push 128901/CachingInDotNet7:latest'
      }
    }

  }
}
