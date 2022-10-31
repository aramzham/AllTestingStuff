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

  }
}