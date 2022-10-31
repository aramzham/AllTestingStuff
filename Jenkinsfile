pipeline {
  agent any
  stages {
    stage('Checkout code') {
      steps {
        git(url: 'https://github.com/aramzham/AllTestingStuff', branch: 'master')
      }
    }

    stage('Add log message') {
      steps {
        sh 'ls -la'
      }
    }

  }
}