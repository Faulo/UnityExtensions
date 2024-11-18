pipeline {
	agent {
		label 'npm'
	}
	environment {
		BRANCH_NAME = 'main'
	}
	stages {
		stage('Linux') {
			steps {
				script {
					unityPackage {
						// define unity package location relative to repository
						PACKAGE_LOCATION = ''
						UNITY_NODE = 'unity && linux'

						// which Unity Test Runner modes to execute
						TEST_MODES = 'EditMode PlayMode'

						// verify that code is formatted
						TEST_FORMATTING = '1'
						EDITORCONFIG_LOCATION = '.jenkins/.editorconfig'

						// which platforms to deploy to
						DEPLOY_TO_VERDACCIO = '0'
					}
				}
			}
		}
		stage('Windows') {
			steps {
				script {
					unityPackage {
						// define unity package location relative to repository
						PACKAGE_LOCATION = ''
						UNITY_NODE = 'unity && windows'

						// which Unity Test Runner modes to execute
						TEST_MODES = 'EditMode PlayMode'

						// verify that code is formatted
						TEST_FORMATTING = '1'
						EDITORCONFIG_LOCATION = '.jenkins/.editorconfig'

						// which platforms to deploy to
						DEPLOY_TO_VERDACCIO = '0'
					}
				}
			}
		}
	}
}