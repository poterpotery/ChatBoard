import React, { useState } from 'react'
import { Image, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native'
import { useNavigation } from '@react-navigation/native'
import { useToast } from 'react-native-toast-notifications'
import { APP_COLORS } from '../../utils/colorContants'
import { useTheme } from '../../hooks'
import ServerApi from '../../AxiosApi'
import { ACTIVE_OPACITY } from '../../utils/constants'

const SignUp = () => {
  const { Images } = useTheme()
  const navigation = useNavigation()
  const toast = useToast()

  const [fullName, setFullName] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [confirmPassword, setConfirmPassword] = useState('')

  const validateEmail = (email) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    return emailRegex.test(email)
  }

  const handleSubmit = () => {


    // Validate email
    if (!validateEmail(email)) {
      toast.show('Please enter a valid email address.', {
        type: 'danger',
        placement: 'top',
        offset: 300,
      })
      return
    }

    // Password matching validation
    if (password !== confirmPassword) {
      toast.show('Passwords do not match.', {
        type: 'danger',
        placement: 'top',
        offset: 300,
      })
      return
    }

    let body = {
      "email": email,
      "password": password,
      "accountType": 1,
      "gender": 1

    }

    ServerApi.post('/Auth/SignUp', body).then((response) => {

      toast.show('Account Created!', {
        type: 'success',
        placement: 'top',
        offset: 300,
      })

      if (navigation.canGoBack()) {
        navigation.goBack()
      }
      else {
        navigation.navigate("Login")
      }

    }).catch((error) => {

      if (error?.data?.errors?.Password[0] == "Password must meet the requirements.") {
        toast.show('Password must meet the requirements.', {
          type: 'danger',
          placement: 'top',
          offset: 300,
        });
      }
      else {
        toast.show('Signup Failed', {
          type: 'danger',
          placement: 'top',
          offset: 300,
        });
      }

    })
    // Further processing

  }

  const [isShowPassword, setIsShowPassword] = useState(false)
  const [isShowConfirmPassword, setIsShowConfirmPassword] = useState(false)


  return (
    <View style={styles.loginContainer}>
      <Text style={styles.header}>Sign-up</Text>

      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          underlineColor="transparent"
          placeholderTextColor={"grey"}
          autoCapitalize="none"
          placeholder={'Full Name'}
          value={fullName}
          onChangeText={setFullName}
        />
        <TextInput
          style={styles.input}
          underlineColor="transparent"
          placeholderTextColor={"grey"}
          autoCapitalize="none"
          placeholder={'Email Address'}
          value={email}
          onChangeText={setEmail}
        />


        <View style={{ width: "100%", alignItems: "center" }}>
          <TextInput
            style={styles.input}
            underlineColor="transparent"
            placeholderTextColor={"grey"}
            autoCapitalize="none"
            placeholder={'Create Password'}
            secureTextEntry={isShowPassword}
            value={password}
            onChangeText={setPassword}
          />

          <TouchableOpacity activeOpacity={ACTIVE_OPACITY}
            onPress={() => {
              setIsShowPassword(!isShowPassword)
            }}
            style={{ position: "absolute", right: "10%", top: "35%" }}>
            {
              isShowPassword ? <Image
                source={require("./eye.png")}
                style={{ height: 20, width: 20, resizeMode: "contain" }}
              /> :
                <Image
                  source={require("./uneye.png")}
                  style={{ height: 20, width: 20, resizeMode: "contain" }}
                />
            }

          </TouchableOpacity>
        </View>

        <View style={{ width: "100%", alignItems: "center" }}>
          <TextInput
            style={styles.input}
            underlineColor="transparent"
            placeholderTextColor={"grey"}
            autoCapitalize="none"
            placeholder={'Confirm Password'}
            secureTextEntry={isShowConfirmPassword}
            value={confirmPassword}
            onChangeText={setConfirmPassword}
          />
          <TouchableOpacity activeOpacity={ACTIVE_OPACITY}
            onPress={() => {
              setIsShowConfirmPassword(!isShowConfirmPassword)
            }}
            style={{ position: "absolute", right: "10%", top: "35%" }}>
            {
              isShowConfirmPassword ? <Image
                source={require("./eye.png")}
                style={{ height: 20, width: 20, resizeMode: "contain" }}
              /> :
                <Image
                  source={require("./uneye.png")}
                  style={{ height: 20, width: 20, resizeMode: "contain" }}
                />
            }

          </TouchableOpacity>
        </View>

      </View>

      <View style={{ marginTop: 40 }}>
        <TouchableOpacity style={styles.btn} onPress={handleSubmit}>
          <Text style={{ fontFamily: "Ubuntu-Bold" }}>Sign-up</Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}

export default SignUp

const styles = StyleSheet.create({
  loginContainer: {
    flex: 1,
    backgroundColor: APP_COLORS.gradientLight,
    alignItems: "center",
    justifyContent: "center"
  },
  image: {
    marginBottom: 50
  },
  inputContainer: {
    width: "100%",
    justifyContent: "center",
    alignItems: "center",
  },
  input: {
    width: "90%",
    height: 50,
    color: "black",
    fontSize: 13,
    borderRadius: 17,
    paddingLeft: 10,
    backgroundColor: "#F0F4F8",
    borderWidth: 1,
    borderColor: "#D3B3B3",
    marginVertical: 10,
    fontFamily: "Ubuntu-Regular"
  },
  btn: {
    paddingVertical: 20,
    paddingHorizontal: 60,
    borderRadius: 20,
    backgroundColor: "#9BC4EC",
    justifyContent: "center",
    alignItems: "center"
  },
  header: {
    fontSize: 30,
    alignSelf: "flex-start",
    paddingLeft: 20,
    fontFamily: "Ubuntu-Bold"
  }
})
