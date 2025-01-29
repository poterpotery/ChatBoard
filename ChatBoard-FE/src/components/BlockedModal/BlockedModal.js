import React from 'react';
import { Linking, Modal, Pressable, StyleSheet, Text, View } from 'react-native';
import { COLORS } from '../../theme/Colors';
import { useTheme } from '../../hooks';
import { TouchableOpacity } from 'react-native-gesture-handler';

const BlockedModal = ({ isVisible, onClose, versionInfo }) => {
  const { CustomColors } = useTheme();
  const openURLInBrowser = url => {
    Linking.openURL(url).catch(err => {
    });
  };
  return (
    <>
      <Modal
        isVisible={isVisible}
        animationIn="slideInUp"
        animationOut="slideOutDown"
        backdropOpacity={0.5}
         transparent={true}
        onBackdropPress={onClose}
        style={{ backgroundColor: 'rgba(0, 0, 0, 0)',}}
      >
        <View style={styles.container}>
          <View style={styles.modalContent}>
            <Text style={[styles.title, { color: CustomColors.gray, fontSize: 15, fontWeight: "600" }]}>
              Account Suspended!
            </Text>
            <Text
              style={[
                styles.description,
                { color: CustomColors.gray, fontSize: 12, textAlign: 'center' },
              ]}
            >
              Oops! It seems like your account has been suspended due to a
              violation of our community guidelines.{' '}
              <Pressable onPress={() => openURLInBrowser("https://google.com")}>
                <Text
                  style={[
                    styles.description, styles.contactSupport,
                    { fontSize: 12, textAlign: 'center', top: 2},
                  ]}
                >
                  Contact Support
                </Text>
              </Pressable>{' '}
              for assistance.
            </Text>

          </View>
        </View>
      </Modal>
    </>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    height: '100%',
    width: '100%',
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: "rgba(0, 0, 0, 0.7)",
    position: 'absolute',
    bottom: 0,
  },
  modalContent: {
    padding: 20,
    borderRadius: 25,
    backgroundColor: COLORS.grayBackground,
    borderWidth: 1,
    borderColor: "white",
    position: 'absolute',
    bottom: 40,
  },
  title: {
    fontSize: 18,
    marginBottom: 10,
    justifyContent: "center", alignItems: "center", alignSelf: "center"
  },
  description: {
    alignItems: "center",
    alignSelf: "center",
  },
  contactSupport: {
    color: '#0d90d3',
    textDecorationLine: 'underline',
  }
});

export default BlockedModal;
