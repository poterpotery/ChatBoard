import { StatusBar, StyleSheet, Text, TouchableOpacity, View } from 'react-native'
import React, { useEffect, useState } from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import Header from '../../components/Header/Header'
import { ACTIVE_OPACITY } from '../../utils/constants'
import { useIsFocused, useNavigation } from '@react-navigation/native'
import AsyncStorage from '@react-native-async-storage/async-storage'

const Profile = () => {
  const navigation = useNavigation()
  const [email, setEmail] = useState("")
  const [firstName, setFirstName] = useState("")
  const [phone, setPhone] = useState("")
  const isFocused = useIsFocused()
  useEffect(() => {
    // Fetch data from AsyncStorage on component mount
    const fetchData = async () => {
      const storedEmail = await AsyncStorage.getItem('Email')
      const storedFirstName = await AsyncStorage.getItem('FirstName')
      const storedPhone = await AsyncStorage.getItem('Phone')

      setEmail(storedEmail || "-")
      setFirstName(storedFirstName || "-")
      setPhone(storedPhone || "-")
    }

    fetchData()
  }, [isFocused])

  return (
    <>
      <StatusBar
        translucent
        backgroundColor="transparent"
        barStyle="dark-content" // Use "dark-content" for dark text/icons
      />
      <Header />
      <Text style={styles.header}>Profile</Text>

      <View style={styles.container}>
        <View style={styles.card}>
          <Text style={styles.text}>Name</Text>
          <Text style={styles.text}>{firstName}</Text>
        </View>
        <View style={styles.card}>
          <Text style={styles.text}>Email</Text>
          <Text style={styles.text}>{email}</Text>
        </View>
        <View style={styles.card}>
          <Text style={styles.text}>Phone</Text>
          <Text style={styles.text}>{phone}</Text>
        </View>
        <TouchableOpacity 
          style={styles.btn} 
          activeOpacity={ACTIVE_OPACITY} 
          onPress={() => {
            navigation.navigate("UpdateProfile", {
              PrevEmail: email,
              PrevName: firstName,
              PrevPhone: phone
            })
          }}
        >
          <Text style={styles.logout}>Update Info</Text>
        </TouchableOpacity>
      </View>
    </>
  )
}

export default Profile

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
    width: "50%",
    backgroundColor: "#4CD964",
    borderRadius: 30,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
    marginTop: 20
  },
  logout: {
    fontSize: 15,
    fontFamily: "Ubuntu-Bold",
  }
})
