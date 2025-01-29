import { ActivityIndicator, Image, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native'
import React, { useState } from 'react'
import { useNavigation } from '@react-navigation/native'
import { useToast } from 'react-native-toast-notifications'
import { APP_COLORS } from '../../utils/colorContants'
import { useTheme } from '../../hooks'
import LogoSvg from '../../components/Svgs/LogoSvg'
import ServerApi from '../../AxiosApi'
import { setToken } from '../../store/authentication'
import { useDispatch } from 'react-redux'
import AsyncStorage from '@react-native-async-storage/async-storage'
import { ACTIVE_OPACITY } from '../../utils/constants'

const Login = () => {
  const { Images } = useTheme()
  const navigation = useNavigation()
  const toast = useToast()
  const dispatch = useDispatch()

  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [isLoading, setIsLoading] = useState(false)
  const handleLogin = () => {
    if (!email || !password) return;
    if (isLoading) return;
    setIsLoading(true)
    let body = {
      "email": email,
      "password": password,
      "accountType": 1
    }
    ServerApi.post('/Auth/SignIn', body).then(async (response) => {
      if (response?.data?.Message == "Account doesn’t exist") {
        toast.show('Login Failed', {
          type: 'danger',
          placement: 'top',
          offset: 300,
        });
        setIsLoading(false)
      }
      else {
        const { Id, Email, FirstName } = await response?.data?.Data?.Account;
        const { Token } = await response?.data?.Data;
        setIsLoading(false)
        // Storing each field in AsyncStorage
        await AsyncStorage.setItem('Id', Id.toString());
        await AsyncStorage.setItem('Email', Email);
        await AsyncStorage.setItem('FirstName', FirstName);
        await AsyncStorage.setItem('Phone', "-");
        await AsyncStorage.setItem('Token', Token);

        dispatch(setToken(Token));
        navigation.navigate("DrawerNavigation");
        toast.show('Login Successful', {
          type: 'success',
          placement: 'top',
          offset: 300,
        });
      }
    }).catch((error) => {
      setIsLoading(false)
      toast.show('Login Failed', {
        type: 'danger',
        placement: 'top',
        offset: 300,
      });
    })

  }

  const [isShowPassword, setIsShowPassword] = useState(false)

  return (
    <View style={styles.loginContainer}>
      {/* <Image
        source={Images.app.logo}
        style={styles.image}
      /> */}
      <LogoSvg />

      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          underlineColor="transparent"
          mode="outlined"
          placeholderTextColor={"grey"}
          autoCapitalize="none"
          placeholder={'Email address'}
          value={email}
          onChangeText={setEmail}
        />
        <View style={{ width: "100%", alignItems: "center" }}>
          <TextInput

            style={styles.input}
            underlineColor="transparent"
            mode="outlined"
            placeholderTextColor={"grey"}
            autoCapitalize="none"
            placeholder={'Password'}
            value={password}
            onChangeText={setPassword}
            secureTextEntry={isShowPassword}
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
        <Text style={{ alignSelf: "flex-start", paddingLeft: 30, color: "#007AFF", fontFamily: "Ubuntu-Medium" }}>Reset password?</Text>
      </View>

      <View style={{ marginTop: 40 }}>
        <TouchableOpacity style={styles.btn} onPress={handleLogin}>
          {
            isLoading ? <ActivityIndicator size={"small"} color={"black"} /> :
              <Text style={{ fontFamily: "Ubuntu-Bold" }}>Login</Text>
          }

        </TouchableOpacity>
        <TouchableOpacity
          style={{ flexDirection: "row", marginTop: 10 }}
          onPress={() => {
            navigation.navigate("SignUp")
          }}
        >
          <Text style={{ fontFamily: "Ubuntu-Bold" }}>Not a member ? </Text>
          <Text style={{ color: "#007AFF", fontFamily: "Ubuntu-Bold" }}>  Sign-up now</Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}

export default Login

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
    marginTop: 20
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
    paddingVertical: 15,
    paddingHorizontal: 60,
    borderRadius: 20,
    backgroundColor: "#9BC4EC",
    justifyContent: "center",
    alignItems: "center"
  }
})
