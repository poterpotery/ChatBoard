import { StatusBar, StyleSheet, Text, TouchableOpacity, View } from 'react-native'
import React from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import Header from '../../components/Header/Header'
import ProfileSettings from '../../components/Svgs/ProfileSettings'
import DeleteSvg from '../../components/Svgs/DeleteSvg'
import { ACTIVE_OPACITY } from '../../utils/constants'
import { useNavigation } from '@react-navigation/native'
import { useToast } from 'react-native-toast-notifications'
import AsyncStorage from '@react-native-async-storage/async-storage'
import { useDispatch } from 'react-redux'
import { setToken } from '../../store/authentication'

const Settings = () => {
  const navigation = useNavigation()
  const toast = useToast();
  const dispatch = useDispatch();
  return (
    <>
      <StatusBar
        translucent
        backgroundColor="transparent"
        barStyle="dark-content" // Use "dark-content" for dark text/icons
      />
      <Header />
      <Text style={styles.header}>Settings</Text>

      <View style={styles.container}>
        <TouchableOpacity activeOpacity={ACTIVE_OPACITY} style={styles.card} onPress={() => { navigation.navigate("Profile") }}>
          <ProfileSettings />
          <Text style={styles.text}>Profile</Text>
        </TouchableOpacity>
        <TouchableOpacity activeOpacity={ACTIVE_OPACITY} style={styles.card} onPress={() => { navigation.navigate("DeleteAccount") }}>
          <DeleteSvg />
          <Text style={styles.text}>Delete Account</Text>
        </TouchableOpacity>
      </View>

      <View style={styles.logoutContainer}>
        <TouchableOpacity style={styles.btn} activeOpacity={ACTIVE_OPACITY} onPress={() => {

          AsyncStorage.removeItem('Id')
          AsyncStorage.removeItem('Email')
          AsyncStorage.removeItem('FirstName')
          AsyncStorage.removeItem('Token')
          

          dispatch(setToken(''));
          toast.show('Logout Successful', {
            type: 'success',
            placement: 'top',
            offset: 300,
          });
          navigation.navigate({
            name: 'Authentication',
            params: {
              screen: 'Login',
            },
          })
        }}>
          <Text style={styles.logout}>Logout</Text>
        </TouchableOpacity>
      </View>

    </>
  )
}

export default Settings

const styles = StyleSheet.create({
  header: {
    fontSize: 25,
    fontFamily: "Ubuntu-Bold",
    padding: 20,
    color: '#333',
    textAlign: "center"
  },
  container: {
    width: "100%",
    justifyContent: "center",
    alignItems: "center",
  },
  card: {
    height: 60,
    width: "80%",
    borderBottomWidth: 1,
    borderBottomColor: "#878181",
    flexDirection: "row",
    alignItems: "center",
  },
  text: {
    fontFamily: "Ubuntu-Regular",
    marginLeft: 10
  },

  logoutContainer: {
    position: "absolute",
    bottom: 20,
    width: "100%",
    alignItems: "center",
    justifyContent: "center",
  },
  btn: {
    height: 40,
    width: "70%",
    backgroundColor: "#9BC4EC",
    borderRadius: 30,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
  },
  logout: {
    fontSize: 15,
    fontFamily: "Ubuntu-Bold",
  }
})